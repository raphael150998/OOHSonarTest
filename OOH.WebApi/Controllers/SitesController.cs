using Microsoft.AspNetCore.Mvc;
using OOH.Data.Models;
using OOH.WebApi.Filters.Attributes;
using OOH.WebApi.Models.Site;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace OOH.WebApi.Controllers
{
    public class SitesController : Controller
    {
        [OhhFilter("ListSites", Data.ActionPermission.Read)]
        public IActionResult Index()
        {
            return View();
        }


        [OhhFilter("Sites", Data.ActionPermission.Create)]
        [OhhFilter("Sites", Data.ActionPermission.Update)]
        public IActionResult CreateUpdate(int id = 0)
        {
            SiteVm sitio = new();
            sitio.SitioId = id;
            return View(sitio);
        }
    }
}
