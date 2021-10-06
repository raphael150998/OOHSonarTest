using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using OOH.Data.Dtos.Site;
using OOH.Data.Models;
using OOH.Data.Repos;
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
        public async Task<IActionResult> GetList()
        {
            return Ok(await _repo.GetList());
        }

        [HttpGet("log")]
        public async Task<IActionResult> GetLogs(int id)
        {
            return Ok(await _repo.GetLogs(id));
        }

        [HttpGet("find")]
        public async Task<IActionResult> Find(int id)
        {
            Sitios site = await _repo.Find(id);

            if (site == null) return NotFound();

            return Ok(site);
        }

        [HttpPost("select2")]
        public async Task<IActionResult> GetListAsSelect2([FromBody] SiteSelect2InputDto model)
        {
            List<string> keys = string.IsNullOrEmpty(model.term) ? new() : model.term.Split(' ').ToList();
            return Ok(await _repo.GetListForSelect2(new Select2PagingInputDto()
            {
                Search = keys,
                CurrentPage = model.page
            }));
        }
    }
}
