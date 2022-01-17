using Microsoft.AspNetCore.Mvc;
using OOH.Data.Models;
using OOH.Data.Repos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace OOH.WebApi.ApiControllers
{
    [Route("api/insurance")]
    [ApiController]
    public class InsuranceApiController : ControllerBase
    {
        private readonly InsuranceTypesRepo _repo;

        public InsuranceApiController(InsuranceTypesRepo repo)
        {
            _repo = repo;
        }

        [HttpGet("dropdown")]
        public async Task<IActionResult> GetListForDropdown()
        {
            List<SegurosTipos> references = (await _repo.Select()).ToList();

            object result = references.Count() > 0 ? references.Select(x => new { Id = x.SeguroId, Name = x.Nombre }) : new List<object>() { };

            return Ok(result);
        }
    }
}
