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
    public class StructureTypesRepository : OOHContext, IBaseRepository<EstructurasTipos>
    {
        private readonly ILogHelper _log;

        public StructureTypesRepository(IWebUserHelper userHelper, ILogHelper log) : base(userHelper)
        {
            _log = log;
        }

        public async Task<ResultClass> AddOrUpdate(EstructurasTipos estructura)
        {
            ResultClass result = new ResultClass();

            string sql = estructura.EstructuraId == 0 ? "INSERT INTO EstructurasTipos(Nombre) VALUES (@Nombre);" : "UPDATE EstructurasTipos SET Nombre = @Nombre;";

            result.data = estructura.EstructuraId == 0 ? await PostData(sql, true, new DynamicParameters(estructura)) : await UpdateData(sql, true, new DynamicParameters(estructura));

            result.state = (int)result.data > 0;

            EstructurasTipos oldVwersion = new();

            if (estructura.EstructuraId > 0)
            {
                oldVwersion = await Find(estructura.EstructuraId);
            }

            await _log.AddLog(new LogDto()
            {
                Descripcion = estructura.EstructuraId == 0 ? "Creación" : $"Actualización {JsonConvert.SerializeObject(oldVwersion)}",
                Entidad = nameof(EstructurasTipos),
                EntidadId = estructura.EstructuraId == 0 ? (int)result.data : estructura.EstructuraId,
            });

            return result;
        }

        public async Task<EstructurasTipos> Find(int id)
        {
            return await FilterData<EstructurasTipos>($"SELECT * FROM EstructurasTipos WHERE EstructuraId = {id}");
        }

        public async Task<IEnumerable<LogOutputDto>> GetLogs(int id)
        {
            return await _log.GetLogs(new LogInputDto(id, nameof(EstructurasTipos)));
        }

        public async Task<bool> Remove(int id)
        {
            await _log.AddLog(new LogDto()
            {
                Descripcion = "Eliminación",
                Entidad = nameof(EstructurasTipos),
                EntidadId = id
            });

            return await (RemoveData($"DELETE FROM EstructurasTipos WHERE EstructuraId = {id}")) > 0;
        }

        public async Task<IEnumerable<EstructurasTipos>> Select(string where = "")
        {
            return await SelectData<EstructurasTipos>("SELECT * FROM EstructurasTipos " + where);
        }
    }
}
