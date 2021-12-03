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
    public class InsuranceTypesRepository : OOHContext, IBaseRepository<SegurosTipos>
    {
        private readonly ILogHelper _log;

        public InsuranceTypesRepository(IWebUserHelper userHelper, ILogHelper log) : base(userHelper)
        {
            _log = log;
        }

        public async Task<ResultClass> AddOrUpdate(SegurosTipos seguro)
        {
            ResultClass result = new ResultClass();

            string sql = seguro.SeguroId == 0 ? "INSERT INTO SegurosTipos(Nombre) VALUES (@Nombre);" : "UPDATE SegurosTipos SET Nombre = @Nombre;";

            result.data = seguro.SeguroId == 0 ? await PostData(sql, true, new DynamicParameters(seguro)) : await UpdateData(sql, true, new DynamicParameters(seguro));

            result.state = (int)result.data > 0;

            SegurosTipos oldVwersion = new();

            if (seguro.SeguroId > 0)
            {
                oldVwersion = await Find(seguro.SeguroId);
            }

            await _log.AddLog(new LogDto()
            {
                Descripcion = seguro.SeguroId == 0 ? "Creación" : $"Actualización {JsonConvert.SerializeObject(oldVwersion)}",
                Entidad = nameof(SegurosTipos),
                EntidadId = seguro.SeguroId == 0 ? (int)result.data : seguro.SeguroId,
            });

            return result;
        }

        public async Task<SegurosTipos> Find(int id)
        {
            return await FilterData<SegurosTipos>($"SELECT * FROM SegurosTipos WHERE SeguroId = {id}");
        }

        public async Task<IEnumerable<LogOutputDto>> GetLogs(int id)
        {
            return await _log.GetLogs(new LogInputDto(id, nameof(SegurosTipos)));
        }

        public async Task<bool> Remove(int id)
        {
            await _log.AddLog(new LogDto()
            {
                Descripcion = "Eliminación",
                Entidad = nameof(SegurosTipos),
                EntidadId = id
            });

            return await (RemoveData($"DELETE FROM SegurosTipos WHERE SeguroId = {id}")) > 0;
        }

        public async Task<IEnumerable<SegurosTipos>> Select()
        {
            return await SelectData<SegurosTipos>("SELECT * FROM SegurosTipos");
        }
    }
}
