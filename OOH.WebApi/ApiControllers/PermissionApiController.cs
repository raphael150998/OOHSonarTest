using Microsoft.AspNetCore.Mvc;
using OOH.Data.Models;
using OOH.Data.Repos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace OOH.WebApi.ApiControllers
{
    [Route("api/permission")]
    [ApiController]
    public class PermissionApiController : ControllerBase
    {
        private readonly PermissionTypesRepo _repo;

        public PermissionApiController(PermissionTypesRepo repo)
        {
            _repo = repo;
        }

        [HttpGet("dropdown")]
        public async Task<IActionResult> GetListForDropdown()
        {
            List<PermisosMunicipalesTipos> references = (await _repo.Select()).ToList();

            object result = references.Count() > 0 ? references.Select(x => new { Id = x.PermisoId, Name = x.Nombre }) : new List<object>() { };

            return Ok(result);
        }
    }
}
