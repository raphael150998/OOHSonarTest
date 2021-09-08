using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using OOH.Data.Models;
using OOH.Data.Repos;
using OOH.WebApi.Filters.Attributes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace OOH.WebApi.Controllers
{
    public class ClientController : BaseController
    {
        // GET: ClientController
        [OhhFilter("Client", Data.ActionPermission.Read)]
        public ActionResult Index()
        {
            return View();
        }


        // GET: ClientController/Create
        [HttpGet]
        [OhhFilter("Client", Data.ActionPermission.Create)]
        public ActionResult CreateUpdate(int id = 0)
        {
            //Clientes clientes = id != 0 ?new ClientRepository().Find(id).Result:new Clientes();
            //clientes.ClienteId = id;
            return View();
        }

    }
}
