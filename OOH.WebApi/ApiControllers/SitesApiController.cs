using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using OOH.Data.Dtos;
using OOH.Data.Dtos.Site;
using OOH.Data.Helpers;
using OOH.Data.Models;
using OOH.Data.Repos;
using OOH.WebApi.Filters.Attributes;
using OOH.WebApi.Models.Site;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace OOH.WebApi.ApiControllers
{
    [Route("api/site")]
    [ApiController]
    public class SitesApiController : ControllerBase
    {
        private readonly SitioRepository _repo;
        private readonly IMapper _mapper;


        public SitesApiController(SitioRepository repo, IMapper mapper)
        {
            _repo = repo;
            _mapper = mapper;
        }

        [HttpGet("select")]
        [OhhFilterAttribute("ListSites", Data.ActionPermission.Read)]
        public async Task<IActionResult> GetList()
        {
            return Ok(await _repo.GetList());
        }

        [HttpGet("log")]
        [OhhFilterAttribute("Sites", Data.ActionPermission.Execute)]
        public async Task<IActionResult> GetLogs(int id)
        {
            return Ok(await _repo.GetLogs(id));
        }

        [HttpGet("find")]
        [OhhFilterAttribute("Sites", Data.ActionPermission.Read)]
        public async Task<IActionResult> Find(int id)
        {
            Sitios site = await _repo.Find(id);

            if (site == null) return NotFound();

            return Ok(site);
        }

        [HttpPost("select2")]
        [OhhFilterAttribute("ListSites", Data.ActionPermission.Read)]
        public async Task<IActionResult> GetListAsSelect2([FromBody] SiteSelect2InputDto model)
        {
            List<string> keys = string.IsNullOrEmpty(model.term) ? new() : model.term.Split(' ').ToList();
            return Ok(await _repo.GetListForSelect2(new Select2PagingInputDto()
            {
                Search = keys,
                CurrentPage = model.page
            }));
        }

        [HttpPost("CreateUpdate")]
        [OhhFilterAttribute("Sites", Data.ActionPermission.Create)]
        [OhhFilterAttribute("Sites", Data.ActionPermission.Update)]
        public async Task<IActionResult> CreateUpdate([FromBody] Sitios model)
        {
            ResultClass response = new ResultClass();

            try
            {
                response = await _repo.AddOrUpdate(model);
            }
            catch (Exception ex)
            {
                response.message = ex.Message;
                response.state = false;
            }

            return Ok(response);
        }

        [HttpPost("Remove")]
        [OhhFilterAttribute("Sites", Data.ActionPermission.Delete)]
        public async Task<IActionResult> Remove([FromBody] Identify<int> obj)
        {
            return Ok(await _repo.Remove(obj.Id));
        }
    }
}
