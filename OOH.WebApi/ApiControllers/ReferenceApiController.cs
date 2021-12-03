using Microsoft.AspNetCore.Mvc;
using OOH.Data.Models;
using OOH.Data.Repos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace OOH.WebApi.ApiControllers
{
    [Route("api/reference")]
    [ApiController]
    public class ReferenceApiController : ControllerBase
    {
        private readonly CommercialReferencesRepository _repo;

        public ReferenceApiController(CommercialReferencesRepository repo)
        {
            _repo = repo;
        }

        [HttpGet("dropdown")]
        public async Task<IActionResult> GetListForDropdown()
        {
            List<ReferenciasComercialesTipos> references = (await _repo.Select()).ToList();

            object result = references.Count() > 0 ? references.Select(x => new { Id = x.ReferenciaId, Name = x.Nombre }) : new List<object>() { };

            return Ok(result);
        }
    }
}
