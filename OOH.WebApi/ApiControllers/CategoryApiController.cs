using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
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
    public class CategoryApiController : BaseApiController
    {
        CategoryRepo _repo;

        public CategoryApiController(CategoryRepo repo)
        {
            _repo = repo;
        }

        [HttpGet]
        [Route("api/client/category/call")]
        [OhhFilter("ListClient", Data.ActionPermission.Read)]
        public async Task<IEnumerable<ClientesCategorias>> Categorias()
        {
            return await _repo.SelectClientCategory();
            //return repo.Select().Result.ToList();
        }

        [HttpGet]
        [Route("api/face/category/get")]
        [OhhFilter("ListFaceCategory", Data.ActionPermission.Read)]
        public async Task<IEnumerable<CarasCategorias>> CarasCategorias()
        {

            return await _repo.SelectFaceCategory();
        }
    }
}
