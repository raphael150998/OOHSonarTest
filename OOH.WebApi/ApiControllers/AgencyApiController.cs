using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using OOH.Data.Interfaces;
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
    }
}
