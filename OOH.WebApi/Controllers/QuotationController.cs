using Microsoft.AspNetCore.Mvc;
using OOH.Data.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace OOH.WebApi.Controllers
{
    public class QuotationController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }  
        public IActionResult CreateUpdate(long id = 0)
        {
            Cotizaciones model = new Cotizaciones();
            model.CotizacionId = id;
            return View(model);
        }

    }
}
