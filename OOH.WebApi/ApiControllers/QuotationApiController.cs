using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using OOH.Data.Dtos;
using OOH.Data.Dtos.Cotizacion;
using OOH.Data.Dtos.Site;
using OOH.Data.Helpers;
using OOH.Data.Models;
using OOH.Data.Repos;
using OOH.WebApi.Filters.Attributes;
using OOH.WebApi.Models.Site;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace OOH.WebApi.ApiControllers
{
    [ApiController]
    public class QuotationApiController : ControllerBase
    {

        private readonly QuotationRepository _repo;
        private readonly FaceRepository _repoFace;
        private readonly SitioRepository _repoSite;
        public QuotationApiController(QuotationRepository repo, FaceRepository repoFace, SitioRepository repoSite)
        {
            _repo = repo;
            _repoFace = repoFace;
            _repoSite = repoSite;
        }

        [Route("api/quotation/get")]
        [OhhFilter("ListQuotation", Data.ActionPermission.Execute)]
        public async Task<IEnumerable<Cotizaciones>> GetCotizaciones()
        {
            return await _repo.Select();
        }


        [HttpPost]
        [Route("api/Quotation/CEdata")]
        [OhhFilter("Quotation", Data.ActionPermission.Create)]
        public async Task<ResultClass> CreateEdit([FromBody] Cotizaciones collection)
        {
            return _repo.AddOrUpdate(collection).Result;
        }

        [HttpGet]
        [Route("api/facequotation/get")]
        public async Task<List<FaceQuotationDto>> FaceQuotations()
        {
            try
            {
                return (List<FaceQuotationDto>)await _repoFace.SelectFace();

            }
            catch (Exception ex)
            {

                return (new List<FaceQuotationDto>());
            }
        }

        [HttpPost]
        [Route("api/Quotation/SaveMD")]
        [OhhFilter("Quotation", Data.ActionPermission.Create)]
        public async Task<ResultClass> SaveMaestroDetalle([FromBody]QuotationDto collection)
        {

            return await _repo.Create(collection);
           
        }
        [HttpGet]
        [Route("api/quotation/detail/find")]
        [OhhFilter("Quotation", Data.ActionPermission.Read)]
        public async Task<QuotationDto> Find(int Idcotizacion)
        {
            QuotationDto modelo = new QuotationDto();
            modelo = await _repo.FindCotizacion(Idcotizacion);
            modelo.LstCaras= _repo.GetCotizacionesDetalles(Idcotizacion).Result.ToList();
            return modelo;

            //Cotizaciones cotizacion = new Cotizaciones();
            //cotizacion = _repo.FindCotizacion(Idcotizacion).Result.FirstOrDefault();
            //modelo.CotizacionId = cotizacion.CotizacionId;
            //modelo.ConsolidaCostos = cotizacion.ConsolidaCostos;
            //modelo.ClienteId = cotizacion.ClienteId;
            //modelo.Comentarios = cotizacion.Comentarios;
            //modelo.EstadoId = cotizacion.EstadoId;
            //modelo.Estado = cotizacion.Estado;
            //modelo.Fecha = cotizacion.Fecha;
            //modelo.AgenciaId = cotizacion.AgenciaId;
            //modelo.AtencionA = cotizacion.AtencionA;
        }


        [HttpPost]
        [Route("api/quotation/detail/remove")]
        [OhhFilter("Quotation", Data.ActionPermission.Delete)]
        public async Task<bool> removeDetail([FromBody]Identify<int> data)
        {
            return await _repo.RemoveDetail(data.Id);
        }

        [HttpPost]
        [Route("api/quotation/remove")]
        [OhhFilter("Quotation", Data.ActionPermission.Delete)]
        public async Task<IActionResult> removeQuotation([FromBody] Identify<int> data)
        {
            return Ok(new ResultClass() { data = _repo.Remove(data.Id).Result });
        }

        [Route("api/quotation/log")]
        public async Task<IActionResult> GetLogs(int id)
        {
            return Ok(await _repo.GetLogs(id));
        }

    }
}
