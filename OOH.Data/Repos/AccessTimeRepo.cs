using Dapper;
using Newtonsoft.Json;
using OOH.Data.Dtos.AccessTime;
using OOH.Data.Dtos.Logs;
using OOH.Data.Helpers;
using OOH.Data.Interfaces;
using OOH.Data.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OOH.Data.Repos
{
    public class AccessTimeRepo : OOHContext
    {
        private readonly ILogHelper _log;
        public AccessTimeRepo(IWebUserHelper userHelper, ILogHelper log) : base(userHelper)
        {
            _log = log;
        }

        public async Task<ResultClass> AddOrUpdate(SitiosAccessTime model)
        {
            ResultClass result = new ResultClass();

            string sql = model.TimeId == 0 ? "INSERT INTO SitiosAccessTime(SitioId, WeekDay, StartTime, EndTime, Active) VALUES (@SitioId, @WeekDay, @StartTime, @EndTime, @Active);" : "UPDATE SitiosAccessTime SET SitioId = @SitioId, WeekDay = @WeekDay, StartTime = @StartTime, EndTime = @EndTime, Active = @Active WHERE TimeId = @TimeId;";

            result.data = model.TimeId == 0 ? await PostData(sql, true, new DynamicParameters(model)) : await UpdateData(sql, true, new DynamicParameters(model));

            result.state = (int)result.data > 0;

            SitiosAccessTime oldVwersion = new();

            if (model.TimeId > 0)
            {
                oldVwersion = await Find(model.TimeId);
            }

            await _log.AddLog(new LogDto()
            {
                Descripcion = model.TimeId == 0 ? "Creación" : $"Actualización",
                Entidad = nameof(SitiosAccessTime),
                EntidadId = model.TimeId == 0 ? (int)result.data : model.TimeId,
                OldVersionJson = model.TimeId == 0 ? "" : $"{JsonConvert.SerializeObject(oldVwersion)}",
            });

            return result;
        }

        public async Task<SitiosAccessTime> Find(long id)
        {
            return await FilterData<SitiosAccessTime>($"SELECT * FROM SitiosAccessTime WHERE TimeId = {id}");
        }

        public async Task<IEnumerable<LogOutputDto>> GetLogs(long id)
        {
            return await _log.GetLogs(new LogInputDto(id, nameof(SitiosAccessTime)));
        }

        public async Task<bool> Remove(long id)
        {
            await _log.AddLog(new LogDto()
            {
                Descripcion = "Eliminación",
                Entidad = nameof(SitiosAccessTime),
                EntidadId = id
            });

            return await (RemoveData($"DELETE FROM SitiosAccessTime WHERE TimeId = {id}")) > 0;
        }

        public async Task<IEnumerable<SitiosAccessTime>> Select()
        {
            return await SelectData<SitiosAccessTime>("SELECT * FROM SitiosAccessTime");
        }

        /// <summary>
        /// Obtiene un listado de AccessTime por medio del id del sitio
        /// </summary>
        /// <returns></returns>
        public async Task<List<SitiosAccessTime>> GetTimesBySitioId(long sitioId)
        {
            return (await SelectData<SitiosAccessTime>($"SELECT * FROM SitiosAccessTime WHERE SitioId = {sitioId};")).ToList();
        }

        public async Task ModifyTimes(List<TimeInputDto> times, long sitioId)
        {
            List<long> timesIdDb = (await SelectData<long>($"SELECT TimeId FROM SitiosAccessTime WHERE SitioId = {sitioId};")).ToList();

            if (timesIdDb.Count > 0)
            {
                string ids = string.Join(',', timesIdDb);

                await _log.AddLog(new LogDto()
                {
                    Descripcion = $"Eliminación de los ids {ids}",
                    Entidad = nameof(SitiosAccessTime),
                    EntidadId = timesIdDb.FirstOrDefault()
                });

                await RemoveData($"DELETE FROM SitiosAccessTime WHERE TimeId in ({ids})");
            }

            foreach (var time in times)
            {
                SitiosAccessTime accessTime = new()
                {
                    SitioId = sitioId,
                    EndTime = time.EndTime,
                    StartTime = time.StartTime,
                    WeekDay = time.DayOfWeek
                };

                await AddOrUpdate(accessTime);
            }
        }
    }
}
