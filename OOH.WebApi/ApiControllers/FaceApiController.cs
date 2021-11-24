using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using OOH.Data.Dtos;
using OOH.Data.Dtos.Caras;
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

        [HttpGet]
        [Route("api/caras/getLst")]
        public async Task<IActionResult> getCarasLst()
        {
            try
            {
                return Ok(await _repo.SelectCaras());

            }
            catch (Exception ex)
            {

                return Ok(new List<FaceDto>());
            }
        }
        [HttpPost]
        [Route("api/caras/CEdata")]
        public async Task<IActionResult> AddOrUpdate([FromBody] Caras Face)
        {
            return Ok(await _repo.AddOrUpdate(Face));
        }
        [HttpPost]
        [Route("api/caras/get")]
        public async Task<IActionResult> getCaras([FromBody] Identify<int> data)
        {
            try
            {
                return Ok( await _repo.FilterFace(data.Id));

            }
            catch (Exception ex)
            {

                return Ok(new Caras());
            }
        }

        [HttpPost]
        [Route("api/face/remove")]
        public async Task<IActionResult> remove([FromBody]Identify<int> data)
        {
            return Ok(await _repo.Remove(data.Id));
        }

        [HttpPost]
        [Route("api/salientes/clickAdd")]
        public async Task<int> AddSaliente([FromBody] CaraSalientes collection)
        {
            return await _repo.AddSaliente(collection);
        }

        [HttpGet]
        [Route("api/face/salientes/get")]
        public async Task<IActionResult> GetSalientes([FromBody] CaraSalientes collection)
        {
            return Ok(await _repo.FindSaliente(collection));
        }

        [HttpPost]
        [Route("api/salientes/clickRemove")]
        public async Task<bool> RemoveSaliente([FromBody]CaraSalientes collection)
        {
              return await _repo.RemoveSaliente(collection.Id);              
        }
    }
}
