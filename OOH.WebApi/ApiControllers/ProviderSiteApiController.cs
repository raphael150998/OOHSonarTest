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
    [Route("api/providerSite")]
    [ApiController]
    public class ProviderSiteApiController : Controller
    {
        private readonly SiteProviderRepo _repo;

        public ProviderSiteApiController(SiteProviderRepo repo)
        {
            _repo = repo;
        }

        [HttpGet("selectProviders")]
        public async Task<IActionResult> GetListBySitioId(long id)
        {
            return Ok(await _repo.SelectBySitioId(id));
        }

        [HttpPost("CreateUpdate")]
        public async Task<IActionResult> CreateUpdate([FromBody] SitiosProveedor model)
        {
            ResultClass response = new ResultClass();

            try
            {
                SitiosProveedor siteProvider = await _repo.Find(model.Id);

                siteProvider = siteProvider ?? new();

                siteProvider.Id = model.Id;
                siteProvider.ProveedorId = model.ProveedorId;
                siteProvider.SitioId = model.SitioId;
                siteProvider.Porcentaje = model.Porcentaje;
                siteProvider.Monto = model.Monto;

                response = await _repo.AddOrUpdate(siteProvider);
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
            SitiosProveedor siteReference = await _repo.Find(id);

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
