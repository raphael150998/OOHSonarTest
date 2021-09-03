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
        CategoryRepository repo;
        [HttpGet]
        [Route("api/category/call")]
        public List<ClientesCategorias> Categorias()
        {
            repo = new CategoryRepository(txtConectionString());
            return repo.Select().Result.ToList();
        }


    }
}
