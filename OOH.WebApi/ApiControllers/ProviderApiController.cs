using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using OOH.Data.Models;
using OOH.Data.Repos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace OOH.WebApi.ApiControllers
{
    [Route("api/provider")]
    [ApiController]
    public class ProviderApiController : ControllerBase
    {
        private readonly ProviderRepository _repo;

        public ProviderApiController(ProviderRepository repo)
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
            List<Proveedores> providers = (await _repo.Select()).ToList();

            object result = providers.Count() > 0 ? providers.Select(x => new { Id = x.ProveedorId, Name = x.Nombre }) : new List<object>() { };

            return Ok(result);
        }

        [HttpGet("select2")]
        public async Task<IActionResult> GetListAsSelect2()
        {
            List<Proveedores> providers = (await _repo.Select()).ToList();

            return Ok();
        }

    }
}
