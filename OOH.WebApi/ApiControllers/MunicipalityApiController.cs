﻿using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using OOH.Data.Models;
using OOH.Data.Repos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace OOH.WebApi.ApiControllers
{
    [ApiController]
    public class MunicipalityApiController : BaseApiController
    {
        MunicipalityRepository repos;
        [HttpGet]
        [Route("api/municipio/call")]
        public List<Municipios> Municipios()
        {
            repos = new MunicipalityRepository(txtConectionString());
            return repos.Select().Result.ToList();
        }
    }
}