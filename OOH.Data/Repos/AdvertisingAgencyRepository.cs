using Dapper;
using OOH.Data.Dtos;
using OOH.Data.Helpers;
using OOH.Data.Interfaces;
using OOH.Data.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json;
using OOH.Data.Dtos.Logs;

namespace OOH.Data.Repos
{
    public class AdvertisingAgencyRepository : OOHContext, IBaseRepository<AgenciasPublicidad>
    {
        private readonly ILogHelper _log;

        public AdvertisingAgencyRepository(IWebUserHelper userHelper, ILogHelper log) : base(userHelper)
        {
            _log = log;
        }

        public async Task<ResultClass> AddOrUpdate(AgenciasPublicidad agencia)
        {

            ResultClass result = new ResultClass();

            string sql = agencia.AgenciaId == 0 ?
                            "INSERT INTO AgenciasPublicidad(Nombre, Comision, Activo) VALUES (@Nombre, @Comision, @Activo)" :
                            "UPDATE AgenciasPublicidad SET Nombre = @Nombre, Comision = @Comision, Activo = @Activo WHERE AgenciaId = @AgenciaId";

            result.data = agencia.AgenciaId == 0 ? await PostData(sql, true, new DynamicParameters(agencia)) : await UpdateData(sql, true, new DynamicParameters(agencia));

            result.state = (int)result.data > 0;

            AgenciasPublicidad oldVwersion = new();

            if (agencia.AgenciaId > 0)
            {
                oldVwersion = await Find(agencia.AgenciaId);
            }

            await _log.AddLog(new LogDto()
            {
                Descripcion = agencia.AgenciaId == 0 ? "Creación" : $"Actualización",
                OldVersionJson = agencia.AgenciaId == 0 ? "" : $"{JsonConvert.SerializeObject(oldVwersion)}",
                Entidad = nameof(AgenciasPublicidad),
                EntidadId = agencia.AgenciaId == 0 ? (int)result.data : agencia.AgenciaId,
            });

            return result;
        }

        public async Task<AgenciasPublicidad> Find(int id)
        {
            return await FilterData<AgenciasPublicidad>($"SELECT * FROM AgenciasPublicidad WHERE AgenciaId = {id}");
        }

        public async Task<IEnumerable<LogOutputDto>> GetLogs(int id)
        {
            return await _log.GetLogs(new LogInputDto(id, nameof(AgenciasPublicidad)));
        }

        public async Task<bool> Remove(int id)
        {
            await _log.AddLog(new LogDto()
            {
                Descripcion = "Eliminación",
                Entidad = nameof(AgenciasPublicidad),
                EntidadId = id
            });

            return await (RemoveData($"DELETE FROM AgenciasPublicidad WHERE AgenciaId = {id}")) > 0;
        }

        public async Task<IEnumerable<AgenciasPublicidad>> Select(string _Where = "")
        {
            return await SelectData<AgenciasPublicidad>("SELECT * FROM AgenciasPublicidad " + _Where);
        }
    }
}
