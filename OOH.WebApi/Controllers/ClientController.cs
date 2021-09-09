using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using OOH.Data.Interfaces;
using OOH.Data.Models;
using OOH.Data.Repos;
using OOH.WebApi.Filters.Attributes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace OOH.WebApi.Controllers
{
    public class ClientController : Controller
    {
        //private readonly IWebUserHelper _userHelper;
        //private readonly ClientRepository _repo;

        //public ClientController(IWebUserHelper userHelper, ClientRepository repo)
        //{
        //    _userHelper = userHelper;
        //    _repo = repo;
        

        // GET: ClientController
        [OhhFilter("Client", Data.ActionPermission.Read)]
        public IActionResult Index()
        {
        return View();
        }


        // GET: ClientController/Create
        [HttpGet]
        [OhhFilter("Client", Data.ActionPermission.Create)]
        public IActionResult CreateUpdate(int id = 0)
        {
            Clientes clientes = new Clientes();
            clientes.ClienteId = id;
            return View(clientes);
        }

    }
}
