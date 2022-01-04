using OOH.Data.Dtos;
using OOH.Data.Dtos.Logs;
using Dapper;
using OOH.Data.Helpers;
using OOH.Data.Interfaces;
using OOH.Data.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using OOH.Data.Dtos.Cotizacion;

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
                            "" :
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
            try
            {
                await _log.AddLog(new LogDto()
                {
                    Descripcion = "Eliminación",
                    Entidad = nameof(Cotizaciones),
                    EntidadId = id
                });

                return await RemoveData($"delete from [dbo].[CotizacionesDetalle] where CotizacionId = {id} ; delete from  [dbo].[Cotizaciones] Where CotizacionId = {id}", false) == 1 ? true : false;

            }
            catch (Exception ex)
            {
                return false;
            }
        }
        public async Task<ResultClass> AddQuotationDetail(CotizacionesDetalle detalle)
        {
            string sql = "";
            return( new ResultClass() { data = await PostData(sql,true,new DynamicParameters(detalle)) });
        }

        public async Task<QuotationDto> FindCotizacion(int Idcotizacion)
        {
            try
            {
                QuotationDto dto = new QuotationDto();
                dto = await FilterData<QuotationDto>($"SELECT t1.CotizacionId, (select t2.NombreComercial from [dbo].[Clientes] t2 where t2.ClienteId = t1.ClienteId) as Cliente, t1.ClienteId ,t1.Fecha,(select t3.Nombre from [dbo].[AgenciasPublicidad] t3 where t3.AgenciaId = t1.AgenciaId) as Agencia, t1.AgenciaId,(select t4.Descripcion from [dbo].[CotizacionesEstados] t4 where t4.EstadoId = t1.EstadoId) as Estado , t1.EstadoId, t1.AtencionA , t1.ConsolidaCostos , t1.UserId ,t1.Comentarios FROM [dbo].[Cotizaciones] t1 WHERE CotizacionId = {Idcotizacion} ");
                dto.User =  SelectData<QuotationDto>($"SELECT [Username] as [User] FROM [dbo].[Usuarios] Where UserId ={dto.UserId}", "data source=192.168.10.238;initial catalog=OOH_Seguridad;user id=jose;password=Tamao1234;MultipleActiveResultSets=True;App=EntityFramework").Result.FirstOrDefault().User;
                return dto;
            }
            catch (Exception ex)
            {

                throw;
            }

        } 
        public async Task<IEnumerable<Cotizaciones>> Select()
        {
            return await SelectData<Cotizaciones>("SELECT t1.CotizacionId, (select t2.NombreComercial from [dbo].[Clientes] t2 where t2.ClienteId = t1.ClienteId) as Cliente, t1.Fecha,(select t3.Nombre from [dbo].[AgenciasPublicidad] t3 where t3.AgenciaId = t1.AgenciaId) as Agencia, (select t4.Descripcion from [dbo].[CotizacionesEstados] t4 where t4.EstadoId = t1.EstadoId) as Estado, t1.AtencionA , t1.ConsolidaCostos, t1.EstadoId FROM [dbo].[Cotizaciones] t1 ", false);

        }

        public async Task<IEnumerable<QuotationDetailDto>> GetCotizacionesDetalles(Int64 IdCotizacion)
        {
            try
            {
                return await SelectData<QuotationDetailDto>("SELECT t1.Id,t1.CotizacionId,t1.CaraId,(Select t2.Codigo  from Caras t2 where t2.CaraId = t1.CaraId) as Codigo,(Select t3.ReferenciaComercial  from Caras t3 where t3.CaraId = t1.CaraId) as referencia" +
        ", (select(Select Direccion from Sitios t5  where t5.SitioId = t4.SitioId) as Direccion  from Caras t4 where t4.CaraId = t1.CaraId) as direccion" +
        $",t1.CostoArrendamiento,t1.CostoImpresion,t1.CostoInstalacion,t1.CostoSaliente,t1.FechaDesde,t1.FechaHasta FROM[dbo].[CotizacionesDetalle] t1 WHERE t1.CotizacionId = {IdCotizacion} ");

            }
            catch (Exception ex)
            {

                throw;
            }
        }
        public async Task<ResultClass> Create(QuotationDto collection)
        {
            try
            {
                Cotizaciones cotizacion = new Cotizaciones();
                cotizacion.AgenciaId = collection.AgenciaId;
                cotizacion.AtencionA = collection.AtencionA;
                cotizacion.ClienteId = collection.ClienteId;
                cotizacion.Comentarios = collection.Comentarios;
                cotizacion.ConsolidaCostos = collection.ConsolidaCostos;
                cotizacion.CotizacionId = collection.CotizacionId != 0 ? collection.CotizacionId : 0;
                cotizacion.EstadoId = collection.CotizacionId != 0 ? collection.EstadoId : 1;
                cotizacion.Fecha = collection.CotizacionId != 0 ? collection.Fecha : DateTime.Now.ToString();

                List<CotizacionesDetalle> LstDetalle = new List<CotizacionesDetalle>();

                foreach (var item in collection.LstCaras)
                {
                    CotizacionesDetalle detalle = new CotizacionesDetalle();
                    detalle.CotizacionId = collection.CotizacionId;
                    detalle.CaraId = item.CaraId;
                    detalle.CostoArrendamiento = item.CostoArrendamiento;
                    detalle.CostoImpresion = item.CostoImpresion;
                    detalle.CostoInstalacion = item.CostoInstalacion;
                    detalle.CostoSaliente = item.CostoSaliente;
                    detalle.FechaDesde = item.FechaDesde;
                    detalle.FechaHasta = item.FechaHasta;
                    if (!ExisteDetalle(collection.CotizacionId, item.CaraId).Result)
                    {

                        LstDetalle.Add(detalle);
                    }
                }

                cotizacion.UserId = collection.CotizacionId == 0 ? _userHelper.GetUserId() : cotizacion.UserId;

                string sqlMaestro = collection.CotizacionId == 0 ?
                                "INSERT INTO Cotizaciones(Fecha,UserId, EstadoId, ClienteId, AgenciaId, AtencionA, Comentarios, ConsolidaCostos) VALUES(@Fecha,@UserId, @EstadoId, @ClienteId, @AgenciaId, @AtencionA, @Comentarios, @ConsolidaCostos)" :
                                "UPDATE Cotizaciones SET Fecha=@Fecha,EstadoId=@EstadoId,ClienteId=@ClienteId, AgenciaId=@AgenciaId,AtencionA=@AtencionA,Comentarios=@Comentarios,ConsolidaCostos=@ConsolidaCostos WHERE CotizacionId = @CotizacionId";
                 
                string sqlDetalle = "INSERT INTO CotizacionesDetalle(CotizacionId,CaraId,CostoArrendamiento,CostoImpresion,CostoInstalacion,CostoSaliente,FechaDesde,FechaHasta) VALUES (@idMaestro,@CaraId,@CostoArrendamiento,@CostoImpresion,@CostoInstalacion,@CostoSaliente,@FechaDesde,@FechaHasta)";
                long result = await TransactionData<Cotizaciones, CotizacionesDetalle>(sqlMaestro, sqlDetalle, cotizacion, LstDetalle, cotizacion.CotizacionId);
                await _log.AddLog(new LogDto()
                {
                    Descripcion = collection.CotizacionId == 0 ? "Creación" : "Actualización",
                    Entidad = nameof(Cotizaciones),
                    EntidadId = collection.CotizacionId == 0 ? (int)result : collection.CotizacionId,
                });

                return new ResultClass() { data = result, condition = cotizacion.Fecha, state = true };
            }
            catch (Exception ex)
            {
                return new ResultClass() { data = 0, state = false };
            }
          
        }
        private async Task<bool> ExisteDetalle(long idCotizacion, long idCara)
        {
            if (idCotizacion == 0)
            {
                return false;
            }
            try
            {
                CotizacionesDetalle detalle = await FilterData<CotizacionesDetalle>($"SELECT  * FROM [OOH_VIVA].[dbo].[CotizacionesDetalle] Where CaraId = {idCara} and CotizacionId = {idCotizacion}");
                if (detalle != null)
                {
                    return true;

                }
                else
                {
                    return false;
                }
            }
            catch (Exception ex)
            {

                return false;

            }
        }

        public async Task<bool> RemoveDetail(int IdDetail)
        {
            return await RemoveData($"Delete From [dbo].[CotizacionesDetalle] Where Id ={IdDetail}") == 1 ? true : false;
        }

    }
}
