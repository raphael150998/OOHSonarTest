using GoogleMapGenerator.Dtos;
using GoogleMapGenerator.Inteface;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Microsoft.Net.Http.Headers;
using OOH.Data.Interfaces;
using OOH.Data.Models;
using OOH.WebApi.Filters.Attributes;
using OOH.WebApi.Helpers;
using OOH.WebApi.Models;
using PowerPointProvider.Dtos;
using PowerPointProvider.Interface;
using Serilog.Context;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Threading.Tasks;

namespace OOH.WebApi.Controllers
{
    public class HomeController : Controller
    {

        private readonly ILogger<HomeController> _logger;
        private readonly IWebHostEnvironment _webHost;
        private readonly IMapGenerator _mapGenerator;
        private readonly IPowerpointProvider _powerPoint;

        //private readonly IProveedorRepository _proveedorRepo;

        public HomeController(ILogger<HomeController> logger, IWebHostEnvironment webHost, IMapGenerator mapGenerator, IPowerpointProvider powerPoint)
        {
            _logger = logger;
            _webHost = webHost;
            _mapGenerator = mapGenerator;
            _powerPoint = powerPoint;
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

        public async Task<IActionResult> Map()
        {
            //return new FileStreamResult(new MemoryStream(await _mapGenerator.GenerateMap((float)13.7029, (float)-89.2433)), new MediaTypeHeaderValue("application/octet-stream"));


            string url = System.IO.Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "PowerpointTemplate");

            List<SlideFaceInputDto> faces = new List<SlideFaceInputDto>();

            for (int i = 0; i < 50; i++)
            {
                faces.Add(new SlideFaceInputDto()
                {
                    Address = $"Direccion de prueba {i}",
                    Available = i % 2 == 0,
                    Code = $"{i:D5}",
                    DailyTraffic = new Random().Next(1, 1000),
                    Direction = "Sentido unico",
                    Height = new Random().Next(20, 30),
                    Width = new Random().Next(15, 25),
                    HiringPrice = new Random().Next(200, 1987),
                    Map = new CoordinatesInputDto()
                    {
                        Latitude = float.Parse($"13,70{new Random().Next(10, 99).ToString()}"),
                        Longitude = float.Parse($"-89,24{new Random().Next(10, 99).ToString()}")
                    },
                    Notes = "Ninguna",
                    PrintPrice = new Random().Next(100, 200),
                    ReferenceImage = new FileInputDto()
                    {
                        Url = "https://www.pngkey.com/png/full/764-7641545_billboard-png-file-download-free-valla-publicitaria-vector.png",
                        MimeType = "image/png",
                        Extension = ".png"
                    },
                    StructureType = "Estructura metalica"
                });
            }

            PresentationInputDto presentation = new PresentationInputDto()
            {
                Client = "Horacio Rios",
                Faces = faces,
                HasCircuit = true
            };

            var filePptx = await _powerPoint.GetPowerpoint(new FileInputDto() { Url = url, FileName = "template", Extension = ".pptx" }, presentation);

            //return File(await _mapGenerator.GenerateMap((float)13.7029, (float)-89.2433), "application/octet-stream", $"Map_Latitud_{13.7029}_longitud_{-89.2433}_{DateTimeOffset.Now}.png");
            return File(filePptx, "application/octet-stream", "power.pptx");
        }
    }
}
