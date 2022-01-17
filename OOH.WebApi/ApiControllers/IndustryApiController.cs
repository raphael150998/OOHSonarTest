using Microsoft.AspNetCore.Mvc;
using OOH.Data.Models;
using OOH.Data.Repos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace OOH.WebApi.ApiControllers
{
    public class IndustryApiController : ControllerBase
    {
        private readonly IndustryRepo _repo;

        public IndustryApiController(IndustryRepo repo)
        {
            _repo = repo;
        }

        [HttpGet("dropdown")]
        public async Task<IActionResult> GetListForDropdown()
        {
            List<Rubros> references = (await _repo.Select()).ToList();

            object result = references.Count() > 0 ? references.Select(x => new { Id = x.RubroId, Name = x.Nombre }) : new List<object>() { };

            return Ok(result);
        }
    }
}
