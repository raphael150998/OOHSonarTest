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
    public class PermissionTypesRepo : OOHContext, IBaseRepo<PermisosMunicipalesTipos>
    {
        private readonly ILogHelper _log;

        public PermissionTypesRepo(IWebUserHelper userHelper, ILogHelper log) : base(userHelper)
        {
            _log = log;
        }

        public async Task<ResultClass> AddOrUpdate(PermisosMunicipalesTipos permiso)
        {
            ResultClass result = new ResultClass();

            string sql = permiso.PermisoId == 0 ? "INSERT INTO PermisosMunicipalesTipos(Nombre) VALUES (@Nombre);" : "UPDATE PermisosMunicipalesTipos SET Nombre = @Nombre WHERE PermisoId = @PermisoId;";

            result.data = permiso.PermisoId == 0 ? await PostData(sql, true, new DynamicParameters(permiso)) : await UpdateData(sql, true, new DynamicParameters(permiso));

            result.state = (int)result.data > 0;

            PermisosMunicipalesTipos oldVwersion = new();

            if (permiso.PermisoId > 0)
            {
                oldVwersion = await Find(permiso.PermisoId);
            }

            await _log.AddLog(new LogDto()
            {
                Descripcion = permiso.PermisoId == 0 ? "Creación" : $"Actualización",
                Entidad = nameof(PermisosMunicipalesTipos),
                EntidadId = permiso.PermisoId == 0 ? (int)result.data : permiso.PermisoId,
                OldVersionJson = permiso.PermisoId == 0 ? "" : $"{JsonConvert.SerializeObject(oldVwersion)}",
            });

            return result;
        }

        public async Task<PermisosMunicipalesTipos> Find(int id)
        {
            return await FilterData<PermisosMunicipalesTipos>($"SELECT * FROM PermisosMunicipalesTipos WHERE PermisoId = {id}");
        }

        public async Task<IEnumerable<LogOutputDto>> GetLogs(int id)
        {
            return await _log.GetLogs(new LogInputDto(id, nameof(PermisosMunicipalesTipos)));
        }

        public async Task<bool> Remove(int id)
        {
            await _log.AddLog(new LogDto()
            {
                Descripcion = "Eliminación",
                Entidad = nameof(PermisosMunicipalesTipos),
                EntidadId = id
            });

            return await (RemoveData($"DELETE FROM PermisosMunicipalesTipos WHERE PermisoId = {id}")) > 0;
        }

        public async Task<IEnumerable<PermisosMunicipalesTipos>> Select()
        {
            return await SelectData<PermisosMunicipalesTipos>("SELECT * FROM PermisosMunicipalesTipos");
        }
    }
}
