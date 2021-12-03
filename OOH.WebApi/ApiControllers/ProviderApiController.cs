using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using OOH.Data.Dtos;
using OOH.Data.Helpers;
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
        [HttpGet("find")]
        public async Task<IActionResult> Get(int id)
        {
            if (id == 0) return NotFound();
            return Ok(await _repo.Find(id));
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

            return Ok(providers);
        }  
        [HttpGet("category")]
        public async Task<IActionResult> GetListCategory()
        {
            List<ProveedoresCategorias> providers = (await _repo.Category()).ToList();

            return Ok(providers);
        }
        [HttpPost("CEdata")]
        public async Task<IActionResult> AddOrUpdate([FromBody] Proveedores Provider)
        {
            return Ok(await _repo.AddOrUpdate(Provider));
        }

        [HttpPost("remove")]
        public async Task<IActionResult> Remove([FromBody] Identify<int> objeto) {
            try
            {
                return Ok(new ResultClass() { data = await _repo.Remove(objeto.Id)});

            }
            catch (Exception ex)
            {
                if (ex.HResult == -2146233088)
                {
                    return Ok(new ResultClass()
                    {
                        data = null,
                        state = false,
                        condition = "fk",
                        //exception = ex,
                        message = "El proveedor posee una relacion"
                    });
                }
                else
                {
                    return Ok(new ResultClass()
                    {
                        data = null,
                        state = false,
                        condition = "error",
                        //exception = ex,
                        message = "No se a logrado guardar"
                    });
                }

            }
        }
    }
}
