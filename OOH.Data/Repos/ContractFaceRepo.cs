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
    public class ContractFaceRepo : OOHContext
    {

        private readonly ILogHelper _log;

        public ContractFaceRepo(IWebUserHelper userHelper, ILogHelper log) : base(userHelper)
        {
            _log = log;
        }

        public async Task<ResultClass> AddOrUpdate(ContratosCaras model)
        {
            ResultClass result = new ResultClass();

            string sql = model.Id == 0 ? "INSERT INTO ContratosCaras (ContratoId ,CaraId ,CostoMensualArrendamiento ,CostoImpresion ,CostoInstalacion ,CostoSaliente ,Desde ,Hasta ,StandBy ,Base ,Altura ,Referencia ,ConsolidaPorSitio ,Activo) VALUES (@ContratoId, @CaraId, @CostoMensualArrendamiento, @CostoImpresion, @CostoInstalacion, @CostoSaliente, @Desde, @Hasta, @StandBy, @Base, @Altura, @Referencia, @ConsolidaPorSitio, @Activo);" : "UPDATE ContratosCaras SET ContratoId = @ContratoId, CaraId = @CaraId, CostoMensualArrendamiento = @CostoMensualArrendamiento, CostoImpresion = @CostoImpresion, CostoInstalacion = @CostoInstalacion, CostoSaliente = @CostoSaliente, Desde = @Desde, Hasta = @Hasta, StandBy = @StandBy, Base = @Base, Altura = @Altura, Referencia = @Referencia, ConsolidaPorSitio = @ConsolidaPorSitio, Activo = @Activo WHERE Id = @Id;";

            result.data = model.Id == 0 ? await PostData(sql, true, new DynamicParameters(model)) : await UpdateData(sql, true, new DynamicParameters(model));

            result.state = (int)result.data > 0;

            ContratosCaras oldVwersion = new();

            if (model.Id > 0)
            {
                oldVwersion = await Find(model.Id);
            }

            await _log.AddLog(new LogDto()
            {
                Descripcion = model.Id == 0 ? "Creación" : $"Actualización",
                Entidad = nameof(ContratosCaras),
                EntidadId = model.Id == 0 ? (int)result.data : model.Id,
                OldVersionJson = model.Id == 0 ? "" : $"{JsonConvert.SerializeObject(oldVwersion)}",
            });

            return result;
        }

        public async Task<ContratosCaras> Find(long id)
        {
            return await FilterData<ContratosCaras>($"SELECT * FROM ContratosCaras WHERE Id = {id}");
        }

        public async Task<IEnumerable<LogOutputDto>> GetLogs(long id)
        {
            return await _log.GetLogs(new LogInputDto(id, nameof(ContratosCaras)));
        }

        public async Task<bool> Remove(long id)
        {
            await _log.AddLog(new LogDto()
            {
                Descripcion = "Eliminación",
                Entidad = nameof(ContratosCaras),
                EntidadId = id
            });

            return await (RemoveData($"DELETE FROM ContratosCaras WHERE Id = {id}")) > 0;
        }

        public async Task<IEnumerable<ContratosCaras>> Select()
        {
            return await SelectData<ContratosCaras>("SELECT * FROM ContratosCaras");
        }
    }
}
