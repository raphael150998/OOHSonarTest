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
    public class SiteProviderRepo : OOHContext
    {
        private readonly ILogHelper _log;

        public SiteProviderRepo(IWebUserHelper userHelper, ILogHelper log) : base(userHelper)
        {
            _log = log;
        }

        public async Task<ResultClass> AddOrUpdate(SitiosProveedor model)
        {
            ResultClass result = new ResultClass();

            string sql = model.Id == 0 ? "INSERT INTO SitiosProveedor(SitioId, ProveedorId, Porcentaje,Monto) VALUES (@SitioId, @ProveedorId, @Porcentaje, @Monto);" : "UPDATE SitiosProveedor SET SitioId = @SitioId, ProveedorId = @ProveedorId, Porcentaje = @Porcentaje, Monto = @Monto WHERE Id = @Id;";

            result.data = model.Id == 0 ? await PostData(sql, true, new DynamicParameters(model)) : await UpdateData(sql, true, new DynamicParameters(model));

            result.state = (int)result.data > 0;

            SitiosProveedor oldVwersion = new();

            if (model.Id > 0)
            {
                oldVwersion = await Find(model.Id);
            }

            await _log.AddLog(new LogDto()
            {
                Descripcion = model.Id == 0 ? "Creación" : $"Actualización",
                Entidad = nameof(SitiosProveedor),
                EntidadId = model.Id == 0 ? (int)result.data : model.Id,
                OldVersionJson = model.Id == 0 ? "" : $"{JsonConvert.SerializeObject(oldVwersion)}",
            });

            return result;
        }

        public async Task<SitiosProveedor> Find(long id)
        {
            return await FilterData<SitiosProveedor>($"SELECT * FROM SitiosProveedor WHERE Id = {id}");
        }

        public async Task<IEnumerable<LogOutputDto>> GetLogs(long id)
        {
            return await _log.GetLogs(new LogInputDto(id, nameof(SitiosProveedor)));
        }

        public async Task<bool> Remove(long id)
        {
            await _log.AddLog(new LogDto()
            {
                Descripcion = "Eliminación",
                Entidad = nameof(SitiosProveedor),
                EntidadId = id
            });

            return await (RemoveData($"DELETE FROM SitiosProveedor WHERE Id = {id}")) > 0;
        }

        public async Task<IEnumerable<SitiosProveedor>> Select()
        {
            return await SelectData<SitiosProveedor>("SELECT * FROM SitiosProveedor");
        }

        /// <summary>
        /// Obtiene un listado con left join de las tablas SitiosProveedor y Proveedores filtrado por el sitio id
        /// </summary>
        /// <param name="sitioId"></param>
        /// <returns></returns>
        public async Task<IEnumerable<ProviderSiteOutputDto>> SelectBySitioId(long sitioId)
        {
            return await SelectData<ProviderSiteOutputDto>($"SELECT s.*, p.Nombre FROM SitiosProveedor s left join Proveedores p on s.ProveedorId = p.ProveedorId WHERE SitioId = {sitioId}");
        }
    }
}
