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
    public class ContractRepo : OOHContext
    {
        private readonly ILogHelper _log;

        public ContractRepo(IWebUserHelper userHelper, ILogHelper log) : base(userHelper)
        {
            _log = log;
        }

        public async Task<ResultClass> AddOrUpdate(Contratos model)
        {
            ResultClass result = new ResultClass();

            string sql = model.ContratoId == 0 ? 
                "INSERT INTO Contratos (Codigo ,EjecutivoId ,AgenciaId ,ClienteId ,ConsolidarCostos ,RequiereInstalacion ,AtencionA ,RubroId ,Marca ,DiasBonificados ,PromocionId ,Observaciones ,FechaCreacion ,Estado ,Activo) VALUES (@Codigo, @EjecutivoId, @AgenciaId, @ClienteId, @ConsolidarCostos, @RequiereInstalacion, @AtencionA, @RubroId, @Marca, @DiasBonificados, @PromocionId, @Observaciones, @FechaCreacion, @Estado, @Activo)" : 
                "UPDATE Contratos SET  Codigo = @Codigo ,EjecutivoId = @EjecutivoId ,AgenciaId = @AgenciaId ,ClienteId = @ClienteId,ConsolidarCostos = @ConsolidarCostos ,RequiereInstalacion = @RequiereInstalacion, AtencionA = @AtencionA ,RubroId = @RubroId ,Marca = @Marca,DiasBonificados = @DiasBonificados,PromocionId = @PromocionId,Observaciones = @Observaciones,FechaCreacion = @FechaCreacion,Estado = @Estado,Activo = @Activo WHERE ContratoId = @ContratoId";

            result.data = model.ContratoId == 0 ? await PostData(sql, true, new DynamicParameters(model)) : await UpdateData(sql, true, new DynamicParameters(model));

            result.state = (int)result.data > 0;

            Contratos oldVwersion = new();

            if (model.ContratoId > 0)
            {
                oldVwersion = await Find(model.ContratoId);
            }

            await _log.AddLog(new LogDto()
            {
                Descripcion = model.ContratoId == 0 ? "Creación" : $"Actualización",
                Entidad = nameof(Contratos),
                EntidadId = model.ContratoId == 0 ? (int)result.data : model.ContratoId,
                OldVersionJson = model.ContratoId == 0 ? "" : $"{JsonConvert.SerializeObject(oldVwersion)}",
            });

            return result;
        }

        public async Task<Contratos> Find(long id)
        {
            return await FilterData<Contratos>($"SELECT * FROM Contratos WHERE ContratoId = {id}");
        }

        public async Task<IEnumerable<LogOutputDto>> GetLogs(long id)
        {
            return await _log.GetLogs(new LogInputDto(id, nameof(Contratos)));
        }

        public async Task<bool> Remove(long id)
        {
            await _log.AddLog(new LogDto()
            {
                Descripcion = "Eliminación",
                Entidad = nameof(Contratos),
                EntidadId = id
            });

            return await (RemoveData($"DELETE FROM Contratos WHERE ContratoId = {id}")) > 0;
        }

        public async Task<IEnumerable<Contratos>> Select()
        {
            return await SelectData<Contratos>("SELECT * FROM Contratos");
        }
    }
}
