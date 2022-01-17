using Microsoft.AspNetCore.Mvc;
using OOH.Data.Dtos;
using OOH.Data.Helpers;
using OOH.Data.Models;
using OOH.Data.Repos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace OOH.WebApi.ApiControllers
{
    [Route("api/restrictionSite")]
    [ApiController]
    public class RestrictionSiteApiController : ControllerBase
    {
        private readonly RestrictionSiteRepo _repo;

        public RestrictionSiteApiController(RestrictionSiteRepo repo)
        {
            _repo = repo;
        }

        [HttpGet("selectRestrictions")]
        public async Task<IActionResult> GetListBySitioId(long id)
        {
            return Ok(await _repo.SelectBySitioId(id));
        }

        [HttpPost("CreateUpdate")]
        public async Task<IActionResult> CreateUpdate([FromBody] SitiosRestriccionesComerciales model)
        {
            ResultClass response = new ResultClass();

            try
            {
                SitiosRestriccionesComerciales siteRestriction = await _repo.Find(model.Id);

                siteRestriction = siteRestriction ?? new();

                siteRestriction.Id = model.Id;
                siteRestriction.RestriccionId = model.RestriccionId;
                siteRestriction.SitioId = model.SitioId;
                siteRestriction.Comentarios = model.Comentarios;

                response = await _repo.AddOrUpdate(siteRestriction);
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
            SitiosRestriccionesComerciales siteRestriction = await _repo.Find(id);

            if (siteRestriction == null) return NotFound();

            return Ok(siteRestriction);
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
