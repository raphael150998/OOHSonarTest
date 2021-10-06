using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using OOH.Data.Dtos;
using OOH.Data.Models;
using OOH.Data.Repos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace OOH.WebApi.ApiControllers
{
    [ApiController]
    public class FaceApiController : ControllerBase
    {
        private readonly FaceRepository _repo;

        public FaceApiController(FaceRepository repo)
        {
            _repo = repo;
        }

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
        public async Task<Caras> getCaras([FromBody] Identify<int> data)
        {
            try
            {
                return await _repo.FindFace(data.Id);

            }
            catch (Exception ex)
            {

                return (new Caras());
            }
        }
    }
}
