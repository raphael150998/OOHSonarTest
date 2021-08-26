using Microsoft.AspNetCore.Http;
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
    public class HomeController : BaseController
    {
        private readonly ILogger<HomeController> _logger;

        private readonly IProveedorRepository _proveedorRepo;

        public HomeController(IHttpContextAccessor httpContextAccessor, ILogger<HomeController> logger, IProveedorRepository proveedorRepo) : base(httpContextAccessor)
        {
            _logger = logger;
            _proveedorRepo = proveedorRepo;
        }

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
