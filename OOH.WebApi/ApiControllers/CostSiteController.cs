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
    [Route("api/costSite")]
    [ApiController]
    public class CostSiteController : ControllerBase
    {
        private readonly CostSiteRepo _repo;

        public CostSiteController(CostSiteRepo repo)
        {
            _repo = repo;
        }

        [HttpGet("selectCosts")]
        public async Task<IActionResult> GetListBySitioId(long id)
        {
            return Ok(await _repo.SelectBySitioId(id));
        }

        [HttpPost("CreateUpdate")]
        public async Task<IActionResult> CreateUpdate([FromBody] SitiosCostos model)
        {
            ResultClass response = new ResultClass();

            try
            {
                SitiosCostos siteCost = await _repo.Find(model.Id);

                siteCost = siteCost ?? new();

                siteCost.Id = model.Id;
                siteCost.CostoId = model.CostoId;
                siteCost.SitioId = model.SitioId;
                siteCost.Porcentaje = model.Porcentaje;
                siteCost.Monto = model.Monto;

                response = await _repo.AddOrUpdate(siteCost);
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
            SitiosCostos siteCost = await _repo.Find(id);

            if (siteCost == null) return NotFound();

            return Ok(siteCost);
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
