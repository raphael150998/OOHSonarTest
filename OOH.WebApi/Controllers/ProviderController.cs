using Microsoft.AspNetCore.Mvc;
using OOH.Data.Models;
using OOH.WebApi.Filters.Attributes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace OOH.WebApi.Controllers
{
    public class ProviderController : Controller
    {
        [OhhFilterAttribute("ListProviders", Data.ActionPermission.Read)]
        public IActionResult Index()
        {
            return View();
        }
        [OhhFilterAttribute("ListProviders", Data.ActionPermission.Read)]
        public IActionResult AddOrUpdate(int id=0)
        {
            Proveedores provider = new Proveedores();
            provider.ProveedorId = id;
            return View(provider);
        }


    }
}
 