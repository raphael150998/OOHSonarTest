using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using OOH.Data.Models;
using OOH.Data.Repos;
using OOH.WebApi.Controllers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace OOH.WebApi.ApiControllers
{
    [ApiController]
    public class ClientApiController : ControllerBase
    {
        BaseController Base = new BaseController();
        ClientRepository _repos;

       [Route("api/Client/Get")]
       [HttpGet]
       public async Task<List<Clientes>> Clientes()
        {
            _repos = new ClientRepository(Base.txtConectionString());
            return  _repos.Select().Result.ToList();
        }
    }
}
