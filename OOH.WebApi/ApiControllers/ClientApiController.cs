using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using OOH.Data.Dtos;
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

        private readonly ClientRepository _repo;

        public ClientApiController(ClientRepository repo)
        {
            _repo = repo;
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
        public async Task<IActionResult> Remove([FromBody] Identify<int> objeto)
        {
            try
            {
                return Ok(new ResultClass() { data = _repo.Remove(objeto.Id).Result });

            }
            catch (Exception ex)
            {
                if (ex.HResult == -2146233088)
                {
                    return Ok( new ResultClass()
                    {

                        data = null,
                        state = false,
                        condition = "fk",
                        exception = ex,
                        message = "El Cliente posee una relacion"
                    });
                }
                else
                {

                    return Ok( new ResultClass()
                    {
                        data = null,
                        state = false,
                        condition = "error",
                        exception = ex,
                        message = "No se a logrado guardar"
                    });
                }

            }
        }


    }
}
