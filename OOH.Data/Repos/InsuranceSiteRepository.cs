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
    public class InsuranceSiteRepository : OOHContext
    {
        private readonly ILogHelper _log;

        public InsuranceSiteRepository(IWebUserHelper userHelper, ILogHelper log) : base(userHelper)
        {
            _log = log;
        }

        public async Task<ResultClass> AddOrUpdate(SitiosSeguros model)
        {
            ResultClass result = new ResultClass();

            string sql = model.Id == 0 ? "INSERT INTO SitiosSeguros(SitioId, SeguroId) VALUES (@SitioId, @SeguroId);" : "UPDATE SitiosSeguros SET SitioId = @SitioId, SeguroId = @SeguroId WHERE Id = @Id;";

            result.data = model.Id == 0 ? await PostData(sql, true, new DynamicParameters(model)) : await UpdateData(sql, true, new DynamicParameters(model));

            result.state = (int)result.data > 0;

            SitiosSeguros oldVwersion = new();

            if (model.Id > 0)
            {
                oldVwersion = await Find(model.Id);
            }

            await _log.AddLog(new LogDto()
            {
                Descripcion = model.Id == 0 ? "Creación" : $"Actualización {JsonConvert.SerializeObject(oldVwersion)}",
                Entidad = nameof(SitiosSeguros),
                EntidadId = model.Id == 0 ? (int)result.data : model.Id,
            });

            return result;
        }

        public async Task<SitiosSeguros> Find(long id)
        {
            return await FilterData<SitiosSeguros>($"SELECT * FROM SitiosSeguros WHERE Id = {id}");
        }

        public async Task<IEnumerable<LogOutputDto>> GetLogs(long id)
        {
            return await _log.GetLogs(new LogInputDto(id, nameof(SitiosSeguros)));
        }

        public async Task<bool> Remove(long id)
        {
            await _log.AddLog(new LogDto()
            {
                Descripcion = "Eliminación",
                Entidad = nameof(SitiosSeguros),
                EntidadId = id
            });

            return await (RemoveData($"DELETE FROM SitiosSeguros WHERE Id = {id}")) > 0;
        }

        public async Task<IEnumerable<SitiosSeguros>> Select()
        {
            return await SelectData<SitiosSeguros>("SELECT * FROM SitiosSeguros");
        }
    }
}
