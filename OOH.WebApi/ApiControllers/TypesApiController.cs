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
    public class TypesApiController : ControllerBase
    {
        readonly TypesRepository _repo;

        public TypesApiController(TypesRepository repo)
        {
            _repo = repo;
        }

        [HttpGet]
        [Route("api/face/type/get")]
        [OhhFilter("ListFaceTypes", Data.ActionPermission.Read)]
        public async Task<IEnumerable<CarasTipos>> CarasTipos()
        {
            return await _repo.SelectCaraTipos();
        }



    }
}
