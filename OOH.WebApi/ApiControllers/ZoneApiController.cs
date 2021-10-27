using Microsoft.AspNetCore.Mvc;
using OOH.Data.Models;
using OOH.Data.Repos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace OOH.WebApi.ApiControllers
{
    [Route("api/zone")]
    [ApiController]
    public class ZoneApiController : ControllerBase
    {
        private readonly ZoneRepository _repo;

        public ZoneApiController(ZoneRepository repo)
        {
            _repo = repo;
        }

        [HttpGet("select")]
        public async Task<IActionResult> GetList()
        {
            return Ok(await _repo.Select());
        }

        [HttpGet("dropdown")]
        public async Task<IActionResult> GetListForDropdown()
        {
            List<Zonas> zones = (await _repo.Select()).ToList();

            object result = zones.Count() > 0 ? zones.Select(x => new { Id = x.ZonaId, Name = x.Nombre }) : new List<object>() { };

            return Ok(result);
        }
    }
}
