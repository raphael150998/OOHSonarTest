using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using OOH.Data.Interfaces;
using OOH.Data.Models;
using OOH.WebApi.Models;
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

        private readonly IProveedorRepository _proveedorRepo;

        public HomeController(IProveedorRepository proveedorRepo, ILogger<HomeController> logger)
        {
            _proveedorRepo = proveedorRepo;
            _logger = logger;
        }

        public IActionResult Index()
        {
            var proveedor = new Proveedores()
            {
                Activo = true,
                CategoriaId = 1,
                Celular = "sadfsdf",
                Codigo = "001",
                Direccion = "asdfsdf",
                Email = "email@test.com",
                Giro = "sadfsdf",
                NIT = "sdfasdf",
                Nombre = "Rafael",
                NRC = "sadfasdfd",
                PersonaJuridica = true,
                Telefono = "fasdfsdf"
            };
            _proveedorRepo.Create(proveedor);
            return View();
        }

        public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
