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
        private readonly MaterialRepository _repoMaterial;
        private readonly CaraMaterialRepository _repoFaceMaterial;

        public FaceApiController(FaceRepository repo, MaterialRepository repoMaterial, CaraMaterialRepository repoFaceMaterial)
        {
            _repo = repo;
            _repoMaterial = repoMaterial;
            _repoFaceMaterial = repoFaceMaterial;
        }

        #region MaestroCaras

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

        [HttpGet]
        [Route("api/caras/get")]
        public async Task<IActionResult> getCaras(int id)
        {
            try
            {
                return Ok(await _repo.FilterFace(id));

            }
            catch (Exception ex)
            {

                return Ok(new Caras());
            }
        }


        [HttpPost]
        [Route("api/face/remove")]
        public async Task<IActionResult> remove([FromBody] Identify<int> data)
        {
            return Ok(await _repo.Remove(data.Id));
        }
        #endregion

        #region CostosCaras


        #endregion

        #region MaterialCaras
        [HttpGet]
        [Route("api/face/materiales/get")]
        public async Task<IActionResult> getFaceMaterial(long id)
        {
            return Ok(await _repoFaceMaterial.Select(id));
        }

        [HttpPost]
        [Route("api/api/materiales/remove")]
        public async Task<IActionResult> removeFaceMaterial(Identify<long> id)
        {
            return Ok(await _repoFaceMaterial.Remove(id.Id));
        }

        [HttpGet]
        [Route("api/Materiales/getLst")]
        public async Task<IActionResult> getMaterialesLst()
        {
            try
            {
                return Ok(await _repoMaterial.Select());

            }
            catch (Exception ex)
            {

                return Ok(new List<FaceDto>());
            }
        }

        [HttpPost]
        [Route("api/face/material/CEdata")]
        public async Task<IActionResult> postcaraMateria([FromBody]CarasMateriales collection)
        {
            return Ok(await _repoFaceMaterial.AddOrUpdate(collection));
        }
        #endregion

        #region SalientesCaras
        [HttpPost]
        [Route("api/salientes/clickAdd")]
        public async Task<int> AddSaliente([FromBody] CaraSalientes collection)
        {
            return await _repo.AddSaliente(collection);
        }

        [HttpGet]
        [Route("api/face/salientes/get")]
        public async Task<IActionResult> GetSalientes(long id)
        {
            var callback = await _repo.FindSaliente(id);
            return Ok(callback);
        }

        [HttpPost]
        [Route("api/salientes/clickRemove")]
        public async Task<bool> RemoveSaliente([FromBody] CaraSalientes collection)
        {
            return await _repo.RemoveSaliente(collection.Id);
        }
        #endregion


       


      
    }
}
