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
    public class PermissionSiteRepository : OOHContext
    {
        private readonly ILogHelper _log;

        public PermissionSiteRepository(IWebUserHelper userHelper, ILogHelper log) : base(userHelper)
        {
            _log = log;
        }

        public async Task<ResultClass> AddOrUpdate(SitiosPermisosMunicipales model)
        {
            ResultClass result = new ResultClass();

            string sql = model.Id == 0 ? "INSERT INTO SitiosPermisosMunicipales (SitioId, PermisoMunicipalId, EstadoId, Monto, FrecuenciaPago, FechaInicio, FechaFin, FechaInicioCuotas, Activo) VAlUES (@SitioId, @PermisoMunicipalId, @EstadoId, @Monto, @FrecuenciaPago, @FechaInicio, @FechaFin, @FechaInicioCuotas, @Activo);" : "UPDATE SitiosPermisosMunicipales SET SitioId = @SitioId, PermisoMunicipalId = @PermisoMunicipalId, EstadoId = @EstadoId, Monto = @Monto, FrecuenciaPago = @FrecuenciaPago, FechaInicio = @FechaInicio, FechaFin = @FechaFin, FechaInicioCuotas = @FechaInicioCuotas, Activo = @Activo WHERE Id = @Id;";

            result.data = model.Id == 0 ? await PostData(sql, true, new DynamicParameters(model)) : await UpdateData(sql, true, new DynamicParameters(model));

            result.state = (int)result.data > 0;

            SitiosPermisosMunicipales oldVwersion = new();

            if (model.Id > 0)
            {
                oldVwersion = await Find(model.Id);
            }

            await _log.AddLog(new LogDto()
            {
                Descripcion = model.Id == 0 ? "Creación" : $"Actualización {JsonConvert.SerializeObject(oldVwersion)}",
                Entidad = nameof(SitiosPermisosMunicipales),
                EntidadId = model.Id == 0 ? (int)result.data : model.Id,
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
    }
}
