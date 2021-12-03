using Microsoft.AspNetCore.Mvc;
using OOH.Data.Models;
using OOH.Data.Repos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace OOH.WebApi.ApiControllers
{
    [Route("api/restriction")]
    [ApiController]
    public class RestrictionApiController : ControllerBase
    {
        private readonly CommercialRestrictionsRepository _repo;

        public RestrictionApiController(CommercialRestrictionsRepository repo)
        {
            _repo = repo;
        }

        [HttpGet("dropdown")]
        public async Task<IActionResult> GetListForDropdown()
        {
            List<RestriccionesComercialesTipos> references = (await _repo.Select()).ToList();

            object result = references.Count() > 0 ? references.Select(x => new { Id = x.RestriccionId, Name = x.Nombre }) : new List<object>() { };

            return Ok(result);
        }
    }
}
