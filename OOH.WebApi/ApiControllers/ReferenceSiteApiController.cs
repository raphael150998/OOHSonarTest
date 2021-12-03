using Microsoft.AspNetCore.Mvc;
using OOH.Data.Dtos;
using OOH.Data.Dtos.Site;
using OOH.Data.Helpers;
using OOH.Data.Models;
using OOH.Data.Repos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace OOH.WebApi.ApiControllers
{
    [Route("api/referenceSite")]
    [ApiController]
    public class ReferenceSiteApiController : ControllerBase
    {
        private readonly ReferenceSiteRepository _repo;

        public ReferenceSiteApiController(ReferenceSiteRepository repo)
        {
            _repo = repo;
        }

        [HttpGet("selectReferences")]
        public async Task<IActionResult> GetListBySitioId(long id)
        {
            return Ok(await _repo.SelectBySitioId(id));
        }

        [HttpPost("CreateUpdate")]
        public async Task<IActionResult> CreateUpdate([FromBody] SitiosReferenciasComerciales model)
        {
            ResultClass response = new ResultClass();

            try
            {
                SitiosReferenciasComerciales siteReference = await _repo.Find(model.Id);

                siteReference = siteReference ?? new();

                siteReference.Id = model.Id;
                siteReference.ReferenciaId = model.ReferenciaId;
                siteReference.SitioId = model.SitioId;
                siteReference.Comentarios = model.Comentarios;

                response = await _repo.AddOrUpdate(siteReference);
            }
            catch (Exception ex)
            {
                response.message = ex.Message;
                response.state = false;
            }

            return Ok(response);
        }

        [HttpGet("Find")]
        public async Task<IActionResult> Get(long id)
        {
            SitiosReferenciasComerciales siteReference = await _repo.Find(id);

            if (siteReference == null) return NotFound();

            return Ok(siteReference);
        }

        [HttpGet("Log")]
        public async Task<IActionResult> GetLogs(long id)
        {
            return Ok(await _repo.GetLogs(id));
        }

        [HttpPost("Remove")]
        public async Task<IActionResult> Remove([FromBody] Identify<long> obj)
        {
            return Ok(await _repo.Remove(obj.Id));
        }
    }
}
