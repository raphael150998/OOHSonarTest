using Microsoft.AspNetCore.Http;
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
    public class CategoryApiController : BaseApiController
    {
        CategoryRepository _repo;

        public CategoryApiController(CategoryRepository repo)
        {
            _repo = repo;
        }

        [HttpGet]
        [Route("api/client/category/call")]
        public async Task<IEnumerable<ClientesCategorias>> Categorias()
        {
            return await _repo.SelectClientCategory();
            //return repo.Select().Result.ToList();
        }

        [HttpGet]
        [Route("api/face/category/get")]
        public async Task<IEnumerable<CarasCategorias>> CarasCategorias()
        {

            return await _repo.SelectFaceCategory();
        }
    }
}
