using Microsoft.AspNetCore.Mvc;
using OOH.Data.Models;
using OOH.WebApi.Models.Site;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace OOH.WebApi.Controllers
{
    public class SitesController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }

        public IActionResult CreateUpdate(int id = 0)
        {
            SiteVm sitio = new();
            sitio.SitioId = id;
            return View(sitio);
        }
    }
}
