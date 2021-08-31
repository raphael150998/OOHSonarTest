using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace OOH.WebApi.Controllers
{
    public class BaseController : Controller
    {
        protected string GetConnectionString()
        {
            return User.Claims.Where(x => x.Type == "Cs").FirstOrDefault().Value;
        }
    }
}
