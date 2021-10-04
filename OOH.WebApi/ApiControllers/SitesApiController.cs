using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using OOH.Data.Models;
using OOH.Data.Repos;
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
    }
}
