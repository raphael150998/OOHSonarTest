using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using OOH.Data.Helpers;
using OOH.Data.Interfaces;
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

        IWebUserHelper _userHelper;
        private readonly ClientRepository _repo;

        public ClientApiController(IWebUserHelper userHelper)
        {
            _userHelper = userHelper;
            _repo = new ClientRepository(userHelper);
        }
        

        [HttpGet]
       [Route("api/Client/Get")]
       public async Task<List<Clientes>> Clientes()
        {
            return  _repo.Select().Result.ToList();
        }

        [HttpGet]
        [Route("api/client/find")]
        public async Task<IActionResult> Cliente(int id)
        {
            if (id == 0) return NotFound();

            return Ok(_repo.Find(id).Result);

        }
        
        [HttpPost]
        [Route("api/client/CEdata")]
        public async Task<ResultClass> CreateEdit([FromBody] Clientes clientes)
        {           
            return _repo.AddOrUpdate(clientes).Result;
        }

        [HttpPost]
        [Route("api/client/remove")]
        public async Task<bool> Remove([FromForm]int id)
        {
            return _repo.Remove(id).Result;
        }


    }
}
