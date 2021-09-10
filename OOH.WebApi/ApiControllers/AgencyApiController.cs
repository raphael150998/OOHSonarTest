﻿using AutoMapper;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using OOH.Data.Helpers;
using OOH.Data.Models;
using OOH.Data.Repos;
using OOH.WebApi.Models.Agency;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace OOH.WebApi.ApiControllers
{
    [Route("api/agency")]
    [ApiController]
    public class AgencyApiController : ControllerBase
    {
        private readonly AdvertisingAgencyRepository _repo;
        private readonly IMapper _mapper;

        public AgencyApiController(AdvertisingAgencyRepository repo, IMapper mapper)
        {
            _repo = repo;
            _mapper = mapper;
        }

        [HttpGet("Select")]
        public async Task<IActionResult> GetList()
        {
            return Ok(await _repo.Select());
        }

        [HttpPost("CreateUpdate")]
        public async Task<IActionResult> CreateUpdate([FromBody] AgencyVm model)
        {
            ResultClass response = new ResultClass();

            try
            {
                AgenciasPublicidad agency = await _repo.Find(model.Id);

                agency = agency ?? new AgenciasPublicidad();

                agency.AgenciaId = model.Id;
                agency.Nombre = model.Name;
                agency.Comision = model.Rate;

                response = await _repo.AddOrUpdate(agency);
            }
            catch (Exception ex)
            {
                response.message = ex.Message;
                response.state = false;
            }

            return Ok(response);
        }


        [HttpGet("Find")]
        public async Task<IActionResult> Get(int id)
        {
            var algo = Request;
            return Ok(_mapper.Map<AgencyVm>(await _repo.Find(id)));
        }


    }
}
