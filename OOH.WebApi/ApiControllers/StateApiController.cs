using Microsoft.AspNetCore.Mvc;
using OOH.Data.Models;
using OOH.Data.Repos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace OOH.WebApi.ApiControllers
{
    [Route("api/state")]
    [ApiController]
    public class StateApiController : ControllerBase
    {
        private readonly StateTypesRepository _repo;

        public StateApiController(StateTypesRepository repo)
        {
            _repo = repo;
        }

        [HttpGet("dropdown")]
        public async Task<IActionResult> GetListForDropdown()
        {
            List<EstadosTipos> references = (await _repo.Select()).ToList();

            object result = references.Count() > 0 ? references.Select(x => new { Id = x.EstadoId, Name = x.Nombre }) : new List<object>() { };

            return Ok(result);
        }
    }
}
