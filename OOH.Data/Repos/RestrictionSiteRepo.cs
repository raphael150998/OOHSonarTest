using Dapper;
using Newtonsoft.Json;
using OOH.Data.Dtos.Logs;
using OOH.Data.Dtos.Site;
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
    public class RestrictionSiteRepo : OOHContext
    {
        private readonly ILogHelper _log;

        public RestrictionSiteRepo(IWebUserHelper userHelper, ILogHelper log) : base(userHelper)
        {
            _log = log;
        }

        public async Task<ResultClass> AddOrUpdate(SitiosRestriccionesComerciales model)
        {
            ResultClass result = new ResultClass();

            string sql = model.Id == 0 ? "INSERT INTO SitiosRestriccionesComerciales(SitioId, RestriccionId, Comentarios) VALUES (@SitioId, @RestriccionId, @Comentarios);" : "UPDATE SitiosRestriccionesComerciales SET SitioId = @SitioId, RestriccionId = @RestriccionId, Comentarios = @Comentarios WHERE Id = @Id;";

            result.data = model.Id == 0 ? await PostData(sql, true, new DynamicParameters(model)) : await UpdateData(sql, true, new DynamicParameters(model));

            result.state = (int)result.data > 0;

            SitiosRestriccionesComerciales oldVwersion = new();

            if (model.Id > 0)
            {
                oldVwersion = await Find(model.Id);
            }

            await _log.AddLog(new LogDto()
            {
                Descripcion = model.Id == 0 ? "Creación" : $"Actualización",
                Entidad = nameof(SitiosRestriccionesComerciales),
                EntidadId = model.Id == 0 ? (int)result.data : model.Id,
                OldVersionJson = model.Id == 0 ? "" : $"{JsonConvert.SerializeObject(oldVwersion)}",
            });

            return result;
        }

        public async Task<SitiosRestriccionesComerciales> Find(long id)
        {
            return await FilterData<SitiosRestriccionesComerciales>($"SELECT * FROM SitiosRestriccionesComerciales WHERE Id = {id}");
        }

        public async Task<IEnumerable<LogOutputDto>> GetLogs(long id)
        {
            return await _log.GetLogs(new LogInputDto(id, nameof(SitiosRestriccionesComerciales)));
        }

        public async Task<bool> Remove(long id)
        {
            await _log.AddLog(new LogDto()
            {
                Descripcion = "Eliminación",
                Entidad = nameof(SitiosRestriccionesComerciales),
                EntidadId = id
            });

            return await (RemoveData($"DELETE FROM SitiosRestriccionesComerciales WHERE Id = {id}")) > 0;
        }

        public async Task<IEnumerable<SitiosRestriccionesComerciales>> Select(string where = "")
        {
            return await SelectData<SitiosRestriccionesComerciales>("SELECT * FROM SitiosRestriccionesComerciales");
        }

        /// <summary>
        /// Obtiene un listado con left join de las tablas SitiosRestriccionesComerciales y RestriccionesComercialesTipos filtrado por el sitio id
        /// </summary>
        /// <param name="sitioId"></param>
        /// <returns></returns>
        public async Task<IEnumerable<RestrictionSiteOutputDto>> SelectBySitioId(long sitioId)
        {
            return await SelectData<RestrictionSiteOutputDto>($"SELECT s.*, r.Nombre FROM SitiosRestriccionesComerciales s left join RestriccionesComercialesTipos r on s.RestriccionId = r.RestriccionId WHERE s.SitioId = {sitioId}");
        }
    }
}
