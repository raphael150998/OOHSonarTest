using Dapper;
using Newtonsoft.Json;
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
    public class IndustryRepo : OOHContext, IBaseRepo<Rubros>
    {
        private readonly ILogHelper _log;

        public IndustryRepo(IWebUserHelper userHelper, ILogHelper log) : base(userHelper)
        {
            _log = log;
        }

        public async Task<ResultClass> AddOrUpdate(Rubros model)
        {
            ResultClass result = new ResultClass();

            string sql = model.RubroId == 0 ? "INSERT INTO Rubros(Nombre, Activo) VALUES (@Nombre, @Activo);" : "UPDATE Rubros SET Nombre = @Nombre, Activo = @Activo WHERE RubroId = @RubroId;";

            result.data = model.RubroId == 0 ? await PostData(sql, true, new DynamicParameters(model)) : await UpdateData(sql, true, new DynamicParameters(model));

            result.state = (int)result.data > 0;

            Rubros oldVwersion = new();

            if (model.RubroId > 0)
            {
                oldVwersion = await Find(model.RubroId);
            }

            await _log.AddLog(new LogDto()
            {
                Descripcion = model.RubroId == 0 ? "Creación" : $"Actualización",
                Entidad = nameof(Rubros),
                EntidadId = model.RubroId == 0 ? (int)result.data : model.RubroId,
                OldVersionJson = model.RubroId == 0 ? "" : $"{JsonConvert.SerializeObject(oldVwersion)}",
            });

            return result;
        }

        public async Task<Rubros> Find(int id)
        {
            return await FilterData<Rubros>($"SELECT * FROM Rubros WHERE RubroId = {id}");
        }

        public async Task<IEnumerable<LogOutputDto>> GetLogs(int id)
        {
            return await _log.GetLogs(new LogInputDto(id, nameof(Rubros)));
        }

        public async Task<bool> Remove(int id)
        {
            await _log.AddLog(new LogDto()
            {
                Descripcion = "Eliminación",
                Entidad = nameof(Rubros),
                EntidadId = id
            });

            return await (RemoveData($"DELETE FROM Rubros WHERE RubroId = {id}")) > 0;
        }

        public async Task<IEnumerable<Rubros>> Select()
        {
            return await SelectData<Rubros>("SELECT * FROM Rubros");
        }
    }
}
