using Microsoft.AspNetCore.Mvc;
using OOH.Data.Models;
using OOH.Data.Repos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace OOH.WebApi.ApiControllers
{
    [Route("api/cost")]
    [ApiController]
    public class CostCenterController : ControllerBase
    {
        private readonly CostCenterRepository _repo;

        public CostCenterController(CostCenterRepository repo)
        {
            _repo = repo;
        }

        [HttpGet("dropdown")]
        public async Task<IActionResult> GetListForDropdown()
        {
            List<CentroCostos> references = (await _repo.Select()).ToList();

            object result = references.Count() > 0 ? references.Select(x => new { Id = x.CostoId, Name = x.Nombre }) : new List<object>() { };

            return Ok(result);
        }
    }
}
