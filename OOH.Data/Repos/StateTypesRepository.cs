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
    public class StateTypesRepository : OOHContext, IBaseRepository<EstadosTipos>
    {
        private readonly ILogHelper _log;

        public StateTypesRepository(IWebUserHelper userHelper, ILogHelper log) : base(userHelper)
        {
            _log = log;
        }

        public async Task<ResultClass> AddOrUpdate(EstadosTipos estado)
        {
            ResultClass result = new ResultClass();

            string sql = estado.EstadoId == 0 ? "INSERT INTO EstadosTipos(Nombre) VALUES (@Nombre);" : "UPDATE EstadosTipos SET Nombre = @Nombre;";

            result.data = estado.EstadoId == 0 ? await PostData(sql, true, new DynamicParameters(estado)) : await UpdateData(sql, true, new DynamicParameters(estado));

            result.state = (int)result.data > 0;

            EstadosTipos oldVwersion = new();

            if (estado.EstadoId > 0)
            {
                oldVwersion = await Find(estado.EstadoId);
            }

            await _log.AddLog(new LogDto()
            {
                Descripcion = estado.EstadoId == 0 ? "Creación" : $"Actualización {JsonConvert.SerializeObject(oldVwersion)}",
                Entidad = nameof(EstadosTipos),
                EntidadId = estado.EstadoId == 0 ? (int)result.data : estado.EstadoId,
            });

            return result;
        }

        public async Task<EstadosTipos> Find(int id)
        {
            return await FilterData<EstadosTipos>($"SELECT * FROM EstadosTipos WHERE EstadoId = {id}");
        }

        public async Task<IEnumerable<LogOutputDto>> GetLogs(int id)
        {
            return await _log.GetLogs(new LogInputDto(id, nameof(EstadosTipos)));
        }

        public async Task<bool> Remove(int id)
        {
            await _log.AddLog(new LogDto()
            {
                Descripcion = "Eliminación",
                Entidad = nameof(EstadosTipos),
                EntidadId = id
            });

            return await (RemoveData($"DELETE FROM EstadosTipos WHERE EstadoId = {id}")) > 0;
        }

        public async Task<IEnumerable<EstadosTipos>> Select(string where = "")
        {
            return await SelectData<EstadosTipos>("SELECT * FROM EstadosTipos " + where);
        }
    }
}
