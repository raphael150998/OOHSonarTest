using Microsoft.AspNetCore.Mvc;
using OOH.Data.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace OOH.WebApi.Controllers
{
    public class FaceController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }

        public IActionResult AddOrUpdate(int id=0)
        {
            Caras face = new Caras();
            face.CaraId = id;
            return View(face);
        }


    }
}
