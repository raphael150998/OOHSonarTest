using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using OOH.Data.Helpers;
using OOH.Data.Interfaces;
using OOH.Data.Models;
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
        private readonly IAdvertisingAgencyRepository _repo;

        public AgencyApiController(IAdvertisingAgencyRepository repo)
        {
            _repo = repo;
        }

        [HttpGet("GetList")]
        public async Task<IActionResult> GetList()
        {
            return Ok(await _repo.Select());
        }

        [HttpPost("CreateUpdate")]
        public async Task<IActionResult> CreateUpdate([FromBody]AgencyVm model)
        {
            ResultClass response = new ResultClass();

            try
            {
                AgenciasPublicidad agency = await _repo.Find(model.Id);

                agency = agency ?? new AgenciasPublicidad();

                agency.AgenciaId = model.Id;
                agency.Nombre = model.Name;
                agency.Comision = model.Rate;

                int id = 0;

                if (agency.AgenciaId == 0)
                {
                    id = await _repo.Create(agency);
                    response.data = id;
                }
                else
                {
                    await _repo.Update(agency);
                }
            }
            catch (Exception ex)
            {
                response.message = ex.Message;
                response.state = false;
            }

            return Ok(response);
        }
    }
}
