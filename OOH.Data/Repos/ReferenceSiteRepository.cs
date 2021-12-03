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
    public class ReferenceSiteRepository : OOHContext
    {
        private readonly ILogHelper _log;

        public ReferenceSiteRepository(IWebUserHelper userHelper, ILogHelper log) : base(userHelper)
        {
            _log = log;
        }

        public async Task<ResultClass> AddOrUpdate(SitiosReferenciasComerciales model)
        {
            ResultClass result = new ResultClass();

            string sql = model.Id == 0 ? "INSERT INTO SitiosReferenciasComerciales(SitioId, RefComercialId, Comentarios) VALUES (@SitioId, @RefComercialId, @Comentarios);" : "UPDATE SitiosReferenciasComerciales SET SitioId = @SitioId, RefComercialId = @RefComercialId, Comentarios = @Comentarios WHERE Id = @Id;";

            result.data = model.Id == 0 ? await PostData(sql, true, new DynamicParameters(model)) : await UpdateData(sql, true, new DynamicParameters(model));

            result.state = (int)result.data > 0;

            SitiosReferenciasComerciales oldVwersion = new();

            if (model.Id > 0)
            {
                oldVwersion = await Find(model.Id);
            }

            await _log.AddLog(new LogDto()
            {
                Descripcion = model.Id == 0 ? "Creación" : $"Actualización {JsonConvert.SerializeObject(oldVwersion)}",
                Entidad = nameof(SitiosReferenciasComerciales),
                EntidadId = model.Id == 0 ? (int)result.data : model.Id,
            });

            return result;
        }

        public async Task<SitiosReferenciasComerciales> Find(long id)
        {
            return await FilterData<SitiosReferenciasComerciales>($"SELECT * FROM SitiosReferenciasComerciales WHERE Id = {id}");
        }

        public async Task<IEnumerable<LogOutputDto>> GetLogs(long id)
        {
            return await _log.GetLogs(new LogInputDto(id, nameof(SitiosReferenciasComerciales)));
        }

        public async Task<bool> Remove(long id)
        {
            await _log.AddLog(new LogDto()
            {
                Descripcion = "Eliminación",
                Entidad = nameof(SitiosReferenciasComerciales),
                EntidadId = id
            });

            return await (RemoveData($"DELETE FROM SitiosReferenciasComerciales WHERE Id = {id}")) > 0;
        }

        public async Task<IEnumerable<SitiosReferenciasComerciales>> Select()
        {
            return await SelectData<SitiosReferenciasComerciales>("SELECT * FROM SitiosReferenciasComerciales");
        }
    }
}
