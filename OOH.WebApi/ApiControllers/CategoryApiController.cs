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
        [Route("api/category/call")]
        public List<ClientesCategorias> Categorias()
        {
            return _repo.Select().Result.ToList();
        }


    }
}
