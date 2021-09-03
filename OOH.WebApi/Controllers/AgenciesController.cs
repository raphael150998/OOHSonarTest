using Microsoft.AspNetCore.Mvc;
using OOH.Data.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace OOH.WebApi.Controllers
{
    public class AgenciesController : Controller
    {
        private readonly IAdvertisingAgencyRepository _repo;

        public AgenciesController(IAdvertisingAgencyRepository repo)
        {
            _repo = repo;
        }

        public async Task<IActionResult> Index()
        {
            return View();
        }
    }
}
