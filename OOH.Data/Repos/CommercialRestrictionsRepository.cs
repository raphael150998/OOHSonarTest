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
    public class CommercialRestrictionsRepository : OOHContext, IBaseRepository<RestriccionesComercialesTipos>
    {
        private readonly ILogHelper _log;

        public CommercialRestrictionsRepository(IWebUserHelper userHelper, ILogHelper log) : base(userHelper)
        {
            _log = log;
        }

        public async Task<ResultClass> AddOrUpdate(RestriccionesComercialesTipos restriccion)
        {
            ResultClass result = new ResultClass();

            string sql = restriccion.RestriccionId == 0 ? "INSERT INTO RestriccionesComercialesTipos(Nombre) VALUES (@Nombre);" : "UPDATE RestriccionesComercialesTipos SET Nombre = @Nombre;";

            result.data = restriccion.RestriccionId == 0 ? await PostData(sql, true, new DynamicParameters(restriccion)) : await UpdateData(sql, true, new DynamicParameters(restriccion));

            result.state = (int)result.data > 0;

            RestriccionesComercialesTipos oldVwersion = new();

            if (restriccion.RestriccionId > 0)
            {
                oldVwersion = await Find(restriccion.RestriccionId);
            }

            await _log.AddLog(new LogDto()
            {
                Descripcion = restriccion.RestriccionId == 0 ? "Creación" : $"Actualización {JsonConvert.SerializeObject(oldVwersion)}",
                Entidad = nameof(RestriccionesComercialesTipos),
                EntidadId = restriccion.RestriccionId == 0 ? (int)result.data : restriccion.RestriccionId,
            });

            return result;
        }

        public async Task<RestriccionesComercialesTipos> Find(int id)
        {
            return await FilterData<RestriccionesComercialesTipos>($"SELECT * FROM RestriccionesComercialesTipos WHERE RestriccionId = {id}");
        }

        public async Task<IEnumerable<LogOutputDto>> GetLogs(int id)
        {
            return await _log.GetLogs(new LogInputDto(id, nameof(RestriccionesComercialesTipos)));
        }

        public async Task<bool> Remove(int id)
        {
            await _log.AddLog(new LogDto()
            {
                Descripcion = "Eliminación",
                Entidad = nameof(RestriccionesComercialesTipos),
                EntidadId = id
            });

            return await (RemoveData($"DELETE FROM RestriccionesComercialesTipos WHERE RestriccionId = {id}")) > 0;
        }

        public async Task<IEnumerable<RestriccionesComercialesTipos>> Select()
        {
            return await SelectData<RestriccionesComercialesTipos>("SELECT * FROM RestriccionesComercialesTipos");
        }
    }
}
