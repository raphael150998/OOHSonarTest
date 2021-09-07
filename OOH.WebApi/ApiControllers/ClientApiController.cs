using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using OOH.Data.Helpers;
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
    public class ClientApiController : BaseApiController
    {
      
        ClientRepository _repos;

       [HttpGet]
       [Route("api/Client/Get")]
       public async Task<List<Clientes>> Clientes()
        {
            _repos = new ClientRepository(txtConectionString());
            return  _repos.Select().Result.ToList();
        }

        [HttpGet]
        [Route("api/client/find")]
        public async Task<Clientes> Cliente(int id)
        {
            _repos = new ClientRepository(txtConectionString());
            return _repos.Find(id).Result;

        }
        
        [HttpPost]
        [Route("api/client/CEdata")]
        public async Task<ResultClass> CreateEdit([FromBody] Clientes clientes)
        {
            _repos = new ClientRepository(txtConectionString(), IdUserLogin());
           
            return _repos.AddOrUpdate(clientes).Result;
        }

        [HttpPost]
        [Route("api/client/remove")]
        public async Task<bool> Remove([FromForm]int id)
        {
            _repos = new ClientRepository(txtConectionString());
            return _repos.Remove(id).Result;
        }


    }
}
