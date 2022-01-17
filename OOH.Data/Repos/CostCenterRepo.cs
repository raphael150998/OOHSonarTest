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
    public class CostCenterRepo : OOHContext, IBaseRepo<CentroCostos>
    {
        private readonly ILogHelper _log;

        public CostCenterRepo(IWebUserHelper userHelper, ILogHelper log) : base(userHelper)
        {
            _log = log;
        }

        public async Task<ResultClass> AddOrUpdate(CentroCostos model)
        {
            ResultClass result = new ResultClass();

            string sql = model.CostoId == 0 ? "INSERT INTO CentroCostos(Nombre) VALUES (@Nombre);" : "UPDATE CentroCostos SET Nombre = @Nombre WHERE CostoId = @CostoId;";

            result.data = model.CostoId == 0 ? await PostData(sql, true, new DynamicParameters(model)) : await UpdateData(sql, true, new DynamicParameters(model));

            result.state = (int)result.data > 0;

            CentroCostos oldVwersion = new();

            if (model.CostoId > 0)
            {
                oldVwersion = await Find(model.CostoId);
            }

            await _log.AddLog(new LogDto()
            {
                Descripcion = model.CostoId == 0 ? "Creación" : $"Actualización",
                Entidad = nameof(SegurosTipos),
                EntidadId = model.CostoId == 0 ? (int)result.data : model.CostoId,
                OldVersionJson = model.CostoId == 0 ? "" : $"{JsonConvert.SerializeObject(oldVwersion)}",
            });

            return result;
        }

        public async Task<CentroCostos> Find(int id)
        {
            return await FilterData<CentroCostos>($"SELECT * FROM CentroCostos WHERE CostoId = {id}");
        }

        public async Task<IEnumerable<LogOutputDto>> GetLogs(int id)
        {
            return await _log.GetLogs(new LogInputDto(id, nameof(CentroCostos)));
        }

        public async Task<bool> Remove(int id)
        {
            await _log.AddLog(new LogDto()
            {
                Descripcion = "Eliminación",
                Entidad = nameof(CentroCostos),
                EntidadId = id
            });

            return await (RemoveData($"DELETE FROM CentroCostos WHERE CostoId = {id}")) > 0;
        }

        public async Task<IEnumerable<CentroCostos>> Select()
        {
            return await SelectData<CentroCostos>("SELECT * FROM CentroCostos");
        }
    }
}
