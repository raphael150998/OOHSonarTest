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
    public class CostSiteRepo : OOHContext
    {
        private readonly ILogHelper _log;

        public CostSiteRepo(IWebUserHelper userHelper, ILogHelper log) : base(userHelper)
        {
            _log = log;
        }

        public async Task<ResultClass> AddOrUpdate(SitiosCostos model)
        {
            ResultClass result = new ResultClass();

            string sql = model.Id == 0 ? "INSERT INTO SitiosCostos(SitioId, CostoId, Porcentaje,Monto) VALUES (@SitioId, @CostoId, @Porcentaje, @Monto);" : "UPDATE SitiosCostos SET SitioId = @SitioId, CostoId = @CostoId, Porcentaje = @Porcentaje, Monto = @Monto WHERE Id = @Id;";

            result.data = model.Id == 0 ? await PostData(sql, true, new DynamicParameters(model)) : await UpdateData(sql, true, new DynamicParameters(model));

            result.state = (int)result.data > 0;

            SitiosCostos oldVwersion = new();

            if (model.Id > 0)
            {
                oldVwersion = await Find(model.Id);
            }

            await _log.AddLog(new LogDto()
            {
                Descripcion = model.Id == 0 ? "Creación" : $"Actualización",
                Entidad = nameof(SitiosCostos),
                EntidadId = model.Id == 0 ? (int)result.data : model.Id,
                OldVersionJson = model.Id == 0 ? "" : $"{JsonConvert.SerializeObject(oldVwersion)}",
            });

            return result;
        }

        public async Task<SitiosCostos> Find(long id)
        {
            return await FilterData<SitiosCostos>($"SELECT * FROM SitiosCostos WHERE Id = {id}");
        }

        public async Task<IEnumerable<LogOutputDto>> GetLogs(long id)
        {
            return await _log.GetLogs(new LogInputDto(id, nameof(SitiosCostos)));
        }

        public async Task<bool> Remove(long id)
        {
            await _log.AddLog(new LogDto()
            {
                Descripcion = "Eliminación",
                Entidad = nameof(SitiosCostos),
                EntidadId = id
            });

            return await (RemoveData($"DELETE FROM SitiosCostos WHERE Id = {id}")) > 0;
        }

        public async Task<IEnumerable<SitiosCostos>> Select()
        {
            return await SelectData<SitiosCostos>("SELECT * FROM SitiosCostos");
        }

        /// <summary>
        /// Obtiene un listado con left join de las tablas SitiosCostos y CentroCostos filtrado por el sitio id
        /// </summary>
        /// <param name="sitioId"></param>
        /// <returns></returns>
        public async Task<IEnumerable<CostSiteOutputDto>> SelectBySitioId(long sitioId)
        {
            return await SelectData<CostSiteOutputDto>($"SELECT s.*, c.Nombre FROM SitiosCostos s left join CentroCostos c on s.CostoId = c.CostoId WHERE SitioId = {sitioId}");
        }
    }
}
