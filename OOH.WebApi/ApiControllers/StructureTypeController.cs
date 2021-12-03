using Microsoft.AspNetCore.Mvc;
using OOH.Data.Models;
using OOH.Data.Repos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace OOH.WebApi.ApiControllers
{
    [Route("api/structureType")]
    [ApiController]
    public class StructureTypeController : ControllerBase
    {
        private readonly StructureTypesRepository _repo;

        public StructureTypeController(StructureTypesRepository repo)
        {
            _repo = repo;
        }

        [HttpGet("dropdown")]
        public async Task<IActionResult> GetListForDropdown()
        {
            List<EstructurasTipos> structures = (await _repo.Select()).ToList();

            object result = structures.Count() > 0 ? structures.Select(x => new { Id = x.EstructuraId, Name = x.Nombre }) : new List<object>() { };

            return Ok(result);
        }
    }
}
