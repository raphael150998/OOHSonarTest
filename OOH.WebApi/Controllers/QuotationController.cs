using Microsoft.AspNetCore.Mvc;
using OOH.Data.Models;
using OOH.WebApi.Filters.Attributes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace OOH.WebApi.Controllers
{
    public class QuotationController : Controller
    {
        [OhhFilter("ListQuotation", Data.ActionPermission.Read)]
        public IActionResult Index()
        {
            return View();
        }  
        [OhhFilter("Quotation", Data.ActionPermission.Create)]
        [OhhFilter("Quotation", Data.ActionPermission.Update)]
        public IActionResult CreateUpdate(long id = 0)
        {
            Cotizaciones model = new Cotizaciones();
            model.CotizacionId = id;
            return View(model);
        }

    }
}
