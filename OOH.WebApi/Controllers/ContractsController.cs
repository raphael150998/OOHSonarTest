using Microsoft.AspNetCore.Mvc;
using OOH.Data.Repos;
using OOH.WebApi.Filters.Attributes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace OOH.WebApi.Controllers
{
    public class ContractsController : Controller
    {
        private readonly ContractRepo _repo;

        public ContractsController(ContractRepo repo)
        {
            _repo = repo;
        }

        [HttpGet("select")]
        [OhhFilter("ListContracts", Data.ActionPermission.Read)]
        public async Task<IActionResult> GetList()
        {
            return Ok();
        }
    }
}
