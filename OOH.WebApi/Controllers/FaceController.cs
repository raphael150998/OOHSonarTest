using Microsoft.AspNetCore.Mvc;
using OOH.Data.Models;
using OOH.WebApi.Filters.Attributes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace OOH.WebApi.Controllers
{
    public class FaceController : Controller
    {
        [OhhFilter("ListFace", Data.ActionPermission.Read)]
        public IActionResult Index()
        {
            return View();
        }
        [OhhFilter("Face", Data.ActionPermission.Create)]
        [OhhFilter("Face", Data.ActionPermission.Update)]
        [HttpGet("Face/AddOrUpdate/{id}")]
        public IActionResult AddOrUpdate(int id=0)
        {
            Caras face = new Caras();
            face.CaraId = id;
            return View(face);
        }


    }
}
