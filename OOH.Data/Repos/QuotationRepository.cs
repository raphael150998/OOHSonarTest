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

namespace OOH.Data.Repos
{
    public class QuotationRepository : OOHContext, IBaseRepository<Cotizaciones>
    {
        private readonly ILogHelper _log;
        public QuotationRepository(IWebUserHelper userHelper, ILogHelper log) : base(userHelper)
        {
            _log = log;
        }


        public async Task<ResultClass> AddOrUpdate(Cotizaciones collection)
        {
            ResultClass result = new ResultClass();

            string sql = collection.CotizacionId == 0 ?
                            "INSERT INTO Cotizaciones(Fecha,EstadoId,ClienteId, AgenciaId,AtencionA,Comentarios,ConsolidaCostos) VALUES (@Fecha,@EstadoId,@ClienteId, @AgenciaId,@AtencionA,@Comentarios,@ConsolidaCostos)" :
                            "UPDATE Cotizaciones SET Fecha=@Fecha,EstadoId=@EstadoId,ClienteId=@ClienteId, AgenciaId=@AgenciaId,AtencionA=@AtencionA,Comentarios=@Comentarios,ConsolidaCostos=@ConsolidaCostos WHERE CotizacionId = @CotizacionId";

            result.data = collection.CotizacionId == 0 ? await PostData(sql, true, new DynamicParameters(collection)) : await UpdateData(sql, true, new DynamicParameters(collection));

            result.state = (int)result.data > 0;

            await _log.AddLog(new LogDto()
            {
                Descripcion = collection.CotizacionId == 0 ? "Creación" : "Actualización",
                Entidad = nameof(Cotizaciones),
                EntidadId = collection.CotizacionId == 0 ? (int)result.data : collection.CotizacionId,
            });

            return result;
        }

        public async Task<Cotizaciones> Find(int Id)
        {
            return await FilterData<Cotizaciones>($"SELECT * FROM [dbo].[Cotizaciones] Where CotizacionId = {Id}",false);
        }

        public async Task<IEnumerable<LogOutputDto>> GetLogs(int id)
        {
            return await _log.GetLogs(new LogInputDto(id,nameof(Cotizaciones)));
        }

        public async Task<bool> Remove(int id)
        {
            await _log.AddLog(new LogDto()
            {
                Descripcion = "Eliminación",
                Entidad = nameof(Clientes),
                EntidadId = id
            });

            return await RemoveData($"delete from  [dbo].[clientes] Where ClienteId = {id}", false) == 1 ? true : false;
        }

        public async Task<IEnumerable<Cotizaciones>> Select(string _Where = "")
        {
            return await SelectData<Cotizaciones>("SELECT * FROM [dbo].[Cotizaciones] " + _Where,false);
        }

        public async Task<ResultClass> AddQuotationDetail(CotizacionesDetalle detalle)
        {
            string sql = "INSERT INTO CotizacionesDetalle(CotizacionId,CaraId,CostoArrendamiento,CostoImpresion,CostoInstalacion,CostoSaliente,FechaDesde,FechaHasta) VALUES (CotizacionId=@CotizacionId,CaraId=@CaraId,CostoArrendamiento=@CostoArrendamiento,CostoImpresion=@CostoImpresion,CostoInstalacion=@CostoInstalacion,CostoSaliente=@CostoSaliente,FechaDesde=@FechaDesde,FechaHasta=@FechaHasta)";
            return( new ResultClass() { data = await PostData(sql,true,new DynamicParameters(detalle)) });
        }
    }
}
