using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using OOH.Data.Interfaces;
using OOH.Data.Models;
using OOH.WebApi.Filters.Attributes;
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

        //private readonly IProveedorRepository _proveedorRepo;

        public HomeController(ILogger<HomeController> logger)
        {
            _logger = logger;
            //_proveedorRepo = proveedorRepo;
        }

        [OhhFilter("", Data.ActionPermission.NoAction)]
        public IActionResult Index()
        {
            //var proveedor = new Proveedores()
            //{
            //    Activo = true,
            //    CategoriaId = 1,
            //    Celular = "gjhjghj",
            //    Codigo = "0dd01",
            //    Direccion = "asdfdssdfsdf",
            //    Email = "email@test.com",
            //    Giro = "sadfsdf",
            //    NIT = "sdfsdfdfnbbasdf",
            //    Nombre = "Rabbbbfael",
            //    NRC = "sadfavbvbvbsdfd",
            //    PersonaJuridica = true,
            //    Telefono = "fasddfgfgbbbblkjhkhfsdf"
            //};
            //_proveedorRepo.Create(proveedor);

            var infoBaby = new
            {
                mensaje = "el mero masizo chepin cristales",
                hora = DateTime.Now,
                userId = User.Identity.Name
            };

            _logger.LogInformation("se ha guardo en log en mogndb papu {@infoBaby}", infoBaby);

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
