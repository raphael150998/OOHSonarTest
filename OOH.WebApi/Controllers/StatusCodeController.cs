using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace OOH.WebApi.Controllers
{
    public class StatusCodeController : Controller
    {
        public IActionResult Index(string code)
        {
            if (string.IsNullOrEmpty(code))
            {
                code = "Unknown.cshtml";
            }
            return View($"/Views/Errors/{code}.cshtml");
        }
    }
}
