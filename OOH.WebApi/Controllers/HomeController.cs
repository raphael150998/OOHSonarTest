using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using OOH.Data.Interfaces;
using OOH.Data.Models;
using OOH.WebApi.Filters.Attributes;
using OOH.WebApi.Helpers;
using OOH.WebApi.Models;
using Serilog.Context;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;

namespace OOH.WebApi.Controllers
{
    public class HomeController : Controller
    {

        private readonly ILogger<HomeController> _logger;
        private readonly IWebHostEnvironment _webHost;

        //private readonly IProveedorRepository _proveedorRepo;

        public HomeController(ILogger<HomeController> logger, IWebHostEnvironment webHost)
        {
            _logger = logger;
            _webHost = webHost;
            //_proveedorRepo = proveedorRepo;
        }

        [OhhFilter("", Data.ActionPermission.NoAction)]
        public IActionResult Index()
        {
            return View();
        }

        public IActionResult Privacy()
        {
            var infoBaby = new
            {
                mensaje = "el mero masizo chepin cristales",
                hora = DateTime.Now,
                userId = User.Identity.Name
            };

            infoBaby = null;

            return View(infoBaby.mensaje);
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
