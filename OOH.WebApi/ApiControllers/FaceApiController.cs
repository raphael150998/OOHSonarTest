using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using OOH.Data.Dtos;
using OOH.Data.Dtos.Caras;
using OOH.Data.Models;
using OOH.Data.Repos;
using OOH.WebApi.Filters.Attributes;
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
        private readonly FacePriceRepository _repoFacePrice;

        public FaceApiController(FaceRepository repo, MaterialRepository repoMaterial, CaraMaterialRepository repoFaceMaterial, FacePriceRepository repoFacePrice)
        {
            _repo = repo;
            _repoMaterial = repoMaterial;
            _repoFaceMaterial = repoFaceMaterial;
            _repoFacePrice = repoFacePrice;
        }

        #region MaestroCaras
        [OhhFilter("ListFace", Data.ActionPermission.Read)]
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
        [OhhFilter("Face", Data.ActionPermission.Update)]
        [OhhFilter("Face", Data.ActionPermission.Create)]
        [HttpPost]
        [Route("api/caras/CEdata")]
        public async Task<IActionResult> AddOrUpdate([FromBody] Caras Face)
        {
            return Ok(await _repo.AddOrUpdate(Face));
        }

        [OhhFilter("ListFace", Data.ActionPermission.Read)]
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

        [OhhFilter("Face", Data.ActionPermission.Delete)]
        [HttpPost]
        [Route("api/face/remove")]
        public async Task<IActionResult> remove([FromBody] Identify<int> data)
        {
            return Ok(await _repo.Remove(data.Id));
        }
        #endregion

        #region CostosCaras

        [HttpGet]
        [Route("api/PriceType/getLst")]
        public async Task<IActionResult> getTypePrices()
        {
            return Ok(await _repoFacePrice.GetType());
        }
        [OhhFilter("ListFace", Data.ActionPermission.Read)]
        [HttpGet]
        [Route("api/priceface/get")]
        public async Task<IActionResult> getFacePrices(long id)
        {
            return Ok(await _repoFacePrice.GetPriceByFace(id));
        }
        [OhhFilter("Face", Data.ActionPermission.Read)]
        [HttpGet]
        [Route("api/priceface/byid")]
        public async Task<IActionResult> getFacePricesbyId(long id)
        {
            return Ok(await _repoFacePrice.findbyid(id));
        }


        [OhhFilter("Face", Data.ActionPermission.Create)]
        [OhhFilter("Face", Data.ActionPermission.Update)]
        [HttpPost]
        [Route("api/priceface/post")]
        public async Task<IActionResult> PostFacePrice(CarasPrecios collection)
        {
            return Ok(await _repoFacePrice.AddOrUpdate(collection));
         
        }
        [OhhFilter("Face", Data.ActionPermission.Delete)]
        [HttpPost]
        [Route("api/priceface/remove")]
        public async Task<IActionResult> RemoveFacePrice(Identify<long> data)
        {
            return Ok(await _repoFacePrice.RemovePrice(data.Id));
         
        }
        #endregion

        #region MaterialCaras
        [OhhFilter("ListFace", Data.ActionPermission.Read)]
        [HttpGet]
        [Route("api/face/materiales/get")]
        public async Task<IActionResult> getFaceMaterial(long id)
        {
            return Ok(await _repoFaceMaterial.Select(id));
        }

        [OhhFilter("ListFace", Data.ActionPermission.Read)]
        [HttpGet]
        [Route("api/face/material/by")]
        public async Task<IActionResult> getFaceMaterialbyId(long id)
        {
            return Ok(await _repoFaceMaterial.findbyid(id));
        }
        [OhhFilter("Face", Data.ActionPermission.Delete)]
        [HttpPost]
        [Route("api/face/materiales/remove")]
        public async Task<IActionResult> removeFaceMaterial(Identify<long> id)
        {
            return Ok(await _repoFaceMaterial.Remove(id.Id));
        }
        [OhhFilter("ListFace", Data.ActionPermission.Read)]
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
        [OhhFilter("Face", Data.ActionPermission.Create)]
        [OhhFilter("Face", Data.ActionPermission.Update)]
        [HttpPost]
        [Route("api/face/material/CEdata")]
        public async Task<IActionResult> postcaraMateria([FromBody]CarasMateriales collection)
        {
            return Ok(await _repoFaceMaterial.AddOrUpdate(collection));
        }
        #endregion

        #region SalientesCaras
        [OhhFilter("Face", Data.ActionPermission.Create)]
        [HttpPost]
        [Route("api/salientes/clickAdd")]
        public async Task<int> AddSaliente([FromBody] CaraSalientes collection)
        {
            return await _repo.AddSaliente(collection);
        }
        [OhhFilter("ListFace", Data.ActionPermission.Read)]
        [HttpGet]
        [Route("api/face/salientes/get")]
        public async Task<IActionResult> GetSalientes(long id)
        {
            var callback = await _repo.FindSaliente(id);
            return Ok(callback);
        }
        [OhhFilter("Face", Data.ActionPermission.Delete)]
        [HttpPost]
        [Route("api/salientes/clickRemove")]
        public async Task<bool> RemoveSaliente([FromBody] CaraSalientes collection)
        {
            return await _repo.RemoveSaliente(collection.Id);
        }
        #endregion


        [Route("api/face/log")]
        public async Task<IActionResult> GetLogs(int id)
        {
            return Ok(await _repo.GetLogs(id));
        }




    }
}
