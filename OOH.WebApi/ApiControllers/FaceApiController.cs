using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using OOH.Data.Dtos;
using OOH.Data.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace OOH.WebApi.ApiControllers
{
    [ApiController]
    public class FaceApiController : ControllerBase
    {
        [HttpPost]
        [Route("api/caras/getLst")]
        public async Task<List<Caras>> getCarasLst()
        {
            try
            {
                return (new List<Caras>());

            }
            catch (Exception ex)
            {

                return (new List<Caras>());
            }
        }
        [HttpPost]
        [Route("api/caras/get")]
        public async Task<Caras> getCaras([FromBody] Identify<string> data)
        {
            try
            {
                return (new Caras());

            }
            catch (Exception ex)
            {

                return (new Caras());
            }
        }
    }
}
