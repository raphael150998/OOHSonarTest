using Microsoft.AspNetCore.Mvc;
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
        public IActionResult CreateUpdate(int id = 0)
        {
            return View();
        }

    }
}
