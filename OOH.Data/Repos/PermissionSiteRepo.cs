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
    public class PermissionSiteRepo : OOHContext
    {
        private readonly ILogHelper _log;

        public PermissionSiteRepo(IWebUserHelper userHelper, ILogHelper log) : base(userHelper)
        {
            _log = log;
        }

        public async Task<ResultClass> AddOrUpdate(SitiosPermisosMunicipales model)
        {
            ResultClass result = new ResultClass();

            string sql = model.Id == 0 ? "INSERT INTO SitiosPermisosMunicipales (SitioId, PermisoId, EstadoId, Monto, FrecuenciaPago, FechaInicio, FechaFin, FechaInicioCuotas, Activo) VAlUES (@SitioId, @PermisoId, @EstadoId, @Monto, @FrecuenciaPago, @FechaInicio, @FechaFin, @FechaInicioCuotas, @Activo);" : "UPDATE SitiosPermisosMunicipales SET SitioId = @SitioId, PermisoId = @PermisoId, EstadoId = @EstadoId, Monto = @Monto, FrecuenciaPago = @FrecuenciaPago, FechaInicio = @FechaInicio, FechaFin = @FechaFin, FechaInicioCuotas = @FechaInicioCuotas, Activo = @Activo WHERE Id = @Id;";

            result.data = model.Id == 0 ? await PostData(sql, true, new DynamicParameters(model)) : await UpdateData(sql, true, new DynamicParameters(model));

            result.state = (int)result.data > 0;

            SitiosPermisosMunicipales oldVwersion = new();

            if (model.Id > 0)
            {
                oldVwersion = await Find(model.Id);
            }

            await _log.AddLog(new LogDto()
            {
                Descripcion = model.Id == 0 ? "Creación" : $"Actualización",
                Entidad = nameof(SitiosPermisosMunicipales),
                EntidadId = model.Id == 0 ? (int)result.data : model.Id,
                OldVersionJson = model.Id == 0 ? "" : $"{JsonConvert.SerializeObject(oldVwersion)}",
            });

            return result;
        }

        public async Task<SitiosPermisosMunicipales> Find(long id)
        {
            return await FilterData<SitiosPermisosMunicipales>($"SELECT * FROM SitiosPermisosMunicipales WHERE Id = {id}");
        }

        public async Task<IEnumerable<LogOutputDto>> GetLogs(long id)
        {
            return await _log.GetLogs(new LogInputDto(id, nameof(SitiosPermisosMunicipales)));
        }

        public async Task<bool> Remove(long id)
        {
            await _log.AddLog(new LogDto()
            {
                Descripcion = "Eliminación",
                Entidad = nameof(SitiosPermisosMunicipales),
                EntidadId = id
            });

            return await (RemoveData($"DELETE FROM SitiosPermisosMunicipales WHERE Id = {id}")) > 0;
        }

        public async Task<IEnumerable<SitiosPermisosMunicipales>> Select()
        {
            return await SelectData<SitiosPermisosMunicipales>("SELECT * FROM SitiosPermisosMunicipales");
        }

        /// <summary>
        /// Obtiene un listado con left join de las tablas SitiosPermisosMunicipales y PermisosMunicipalesTipos y EstadosTipos filtrado por el sitio id
        /// </summary>
        /// <param name="sitioId"></param>
        /// <returns></returns>
        public async Task<IEnumerable<PermissionSiteOutputDto>> SelectBySitioId(long sitioId)
        {
            return await SelectData<PermissionSiteOutputDto>($"SELECT s.*, p.Nombre NombrePermiso, e.Nombre NombreEstado FROM SitiosPermisosMunicipales s left join PermisosMunicipalesTipos p on p.PermisoId = s.PermisoId left join EstadosTipos e on s.EstadoId = e.EstadoId WHERE SitioId = {sitioId}");
        }
    }
}
