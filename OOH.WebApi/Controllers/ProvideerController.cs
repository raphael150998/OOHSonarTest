using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace OOH.WebApi.Controllers
{
    public class ProvideerController : Controller
    {
        private Microsoft.AspNetCore.Http.IHttpContextAccessor HttpContextAccessor;
        public IActionResult Index()
        {
            return View();
        }
    }
}
 