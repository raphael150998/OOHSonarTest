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
    [Route("api/permissionSite")]
    [ApiController]
    public class PermissionSiteApiController : Controller
    {
        private readonly PermissionSiteRepository _repo;

        public PermissionSiteApiController(PermissionSiteRepository repo)
        {
            _repo = repo;
        }

        [HttpGet("selectPermissions")]
        public async Task<IActionResult> GetListBySitioId(long id)
        {
            return Ok(await _repo.SelectBySitioId(id));
        }

        [HttpPost("CreateUpdate")]
        public async Task<IActionResult> CreateUpdate([FromBody] SitiosPermisosMunicipales model)
        {
            ResultClass response = new ResultClass();

            try
            {
                SitiosPermisosMunicipales sitePermission = await _repo.Find(model.Id);

                sitePermission = sitePermission ?? new();

                sitePermission.Id = model.Id;
                sitePermission.PermisoId = model.PermisoId;
                sitePermission.SitioId = model.SitioId;
                sitePermission.EstadoId = model.EstadoId;
                sitePermission.Monto = model.Monto;
                sitePermission.FrecuenciaPago = model.FrecuenciaPago;
                sitePermission.FechaInicio = model.FechaInicio;
                sitePermission.FechaFin = model.FechaFin;
                sitePermission.FechaInicioCuotas = model.FechaInicioCuotas;
                sitePermission.Activo = model.Activo;

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

            return Ok(sitePermission);
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
