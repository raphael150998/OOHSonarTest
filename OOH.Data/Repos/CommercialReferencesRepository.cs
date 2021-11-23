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
    public class CommercialReferencesRepository : OOHContext, IBaseRepository<ReferenciasComercialesTipos>
    {

        private readonly ILogHelper _log;

        public CommercialReferencesRepository(IWebUserHelper userHelper, ILogHelper log) : base(userHelper)
        {
            _log = log;
        }

        public async Task<ResultClass> AddOrUpdate(ReferenciasComercialesTipos referencia)
        {
            ResultClass result = new ResultClass();

            string sql = referencia.ReferenciaId == 0 ? "INSERT INTO ReferenciasComercialesTipos(Nombre) VALUES (@Nombre);" : "UPDATE ReferenciasComercialesTipos SET Nombre = @Nombre;";

            result.data = referencia.ReferenciaId == 0 ? await PostData(sql, true, new DynamicParameters(referencia)) : await UpdateData(sql, true, new DynamicParameters(referencia));

            result.state = (int)result.data > 0;

            ReferenciasComercialesTipos oldVwersion = new();

            if (referencia.ReferenciaId > 0)
            {
                oldVwersion = await Find(referencia.ReferenciaId);
            }

            await _log.AddLog(new LogDto()
            {
                Descripcion = referencia.ReferenciaId == 0 ? "Creación" : $"Actualización {JsonConvert.SerializeObject(oldVwersion)}",
                Entidad = nameof(ReferenciasComercialesTipos),
                EntidadId = referencia.ReferenciaId == 0 ? (int)result.data : referencia.ReferenciaId,
            });

            return result;
        }

        public async Task<ReferenciasComercialesTipos> Find(int id)
        {
            return await FilterData<ReferenciasComercialesTipos>($"SELECT * FROM ReferenciasComercialesTipos WHERE ReferenciaId = {id}");
        }

        public async Task<IEnumerable<LogOutputDto>> GetLogs(int id)
        {
            return await _log.GetLogs(new LogInputDto(id, nameof(ReferenciasComercialesTipos)));
        }

        public async Task<bool> Remove(int id)
        {
            await _log.AddLog(new LogDto()
            {
                Descripcion = "Eliminación",
                Entidad = nameof(ReferenciasComercialesTipos),
                EntidadId = id
            });

            return await (RemoveData($"DELETE FROM ReferenciasComercialesTipos WHERE ReferenciaId = {id}")) > 0;
        }

        public async Task<IEnumerable<ReferenciasComercialesTipos>> Select()
        {
            return await SelectData<ReferenciasComercialesTipos>("SELECT * FROM ReferenciasComercialesTipos");
        }
    }
}
