using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using OOH.Data.Dtos.Cotizacion;
using OOH.Data.Helpers;
using OOH.Data.Models;
using OOH.Data.Repos;
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
        public QuotationApiController(QuotationRepository repo, FaceRepository repoFace)
        {
            _repo = repo;
            _repoFace = repoFace;
        }

        [Route("api/Client/Get")]
        public async Task<List<Cotizaciones>> GetCotizaciones()
        {
            return _repo.Select().Result.ToList();
        }


        [HttpPost]
        [Route("api/Quotation/CEdata")]
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
    }
}
