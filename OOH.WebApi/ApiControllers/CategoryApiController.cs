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
        private readonly CategoryRepository repo;

        public CategoryApiController(CategoryRepository repo)
        {
            this.repo = repo;
        }

        public CategoryApiController()
        {
        }

        [HttpGet]
        [Route("api/category/call")]
        public List<ClientesCategorias> Categorias()
        {
            return repo.Select().Result.ToList();
        }


    }
}
