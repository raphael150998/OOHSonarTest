using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using OOH.Data.Dtos;
using OOH.Data.Helpers;
using OOH.Data.Models;
using OOH.Data.Repos;
using OOH.WebApi.Models.Site.Permission;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace OOH.WebApi.ApiControllers
{
    [Route("api/permissionSite")]
    [ApiController]
    public class PermissionSiteApiController : Controller
    {
        private readonly PermissionSiteRepo _repo;
        private readonly IMapper _mapper;

        public PermissionSiteApiController(PermissionSiteRepo repo, IMapper mapper)
        {
            _repo = repo;
            _mapper = mapper;
        }

        [HttpGet("selectPermissions")]
        public async Task<IActionResult> GetListBySitioId(long id)
        {
            return Ok(await _repo.SelectBySitioId(id));
        }

        [HttpPost("CreateUpdate")]
        public async Task<IActionResult> CreateUpdate([FromBody] SitePermissionVm model)
        {
            ResultClass response = new ResultClass();

            try
            {
                SitiosPermisosMunicipales sitePermission = await _repo.Find(model.Id);

                sitePermission = sitePermission ?? new();

                sitePermission = _mapper.Map<SitiosPermisosMunicipales>(model);

                response = await _repo.AddOrUpdate(sitePermission);
            }
            catch (Exception ex)
            {
                response.message = ex.Message;
                response.state = false;
            }

            return Ok(response);
        }

        [HttpGet("Find")]
        public async Task<IActionResult> Get(long id)
        {
            SitiosPermisosMunicipales sitePermission = await _repo.Find(id);

            if (sitePermission == null) return NotFound();

            return Ok(_mapper.Map<SitePermissionVm>(sitePermission));
        }

        [HttpGet("Log")]
        public async Task<IActionResult> GetLogs(long id)
        {
            return Ok(await _repo.GetLogs(id));
        }

        [HttpPost("Remove")]
        public async Task<IActionResult> Remove([FromBody] Identify<long> obj)
        {
            return Ok(await _repo.Remove(obj.Id));
        }
    }
}
