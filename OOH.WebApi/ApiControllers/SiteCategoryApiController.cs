using Microsoft.AspNetCore.Mvc;
using OOH.Data.Models;
using OOH.Data.Repos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace OOH.WebApi.ApiControllers
{
    [Route("api/siteCategory")]
    [ApiController]
    public class SiteCategoryApiController : ControllerBase
    {
        private readonly SiteCategoryRepository _repo;

        public SiteCategoryApiController(SiteCategoryRepository repo)
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
            List<SitiosCategorias> categories = (await _repo.Select()).ToList();

            object result = categories.Count() > 0 ? categories.Select(x => new { Id = x.CategoriaId, Name = x.Nombre }) : new List<object>() { };

            return Ok(result);
        }
    }
}
