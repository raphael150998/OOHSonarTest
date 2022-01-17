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
    [Route("api/insuranceSite")]
    [ApiController]
    public class InsuranceSiteApiController : ControllerBase
    {
        private readonly InsuranceSiteRepo _repo;

        public InsuranceSiteApiController(InsuranceSiteRepo repo)
        {
            _repo = repo;
        }

        [HttpGet("selectInsurances")]
        public async Task<IActionResult> GetListBySitioId(long id)
        {
            return Ok(await _repo.SelectBySitioId(id));
        }

        [HttpPost("CreateUpdate")]
        public async Task<IActionResult> CreateUpdate([FromBody] SitiosSeguros model)
        {
            ResultClass response = new ResultClass();

            try
            {
                SitiosSeguros siteInsurance = await _repo.Find(model.Id);

                siteInsurance = siteInsurance ?? new();

                siteInsurance.Id = model.Id;
                siteInsurance.SeguroId = model.SeguroId;
                siteInsurance.SitioId = model.SitioId;

                response = await _repo.AddOrUpdate(siteInsurance);
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
            SitiosSeguros siteInsurance = await _repo.Find(id);

            if (siteInsurance == null) return NotFound();

            return Ok(siteInsurance);
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
