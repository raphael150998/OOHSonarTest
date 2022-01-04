using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using OOH.Data.Helpers;
using OOH.Data.Interfaces;
using OOH.Data.Models;
using OOH.WebApi.Filters.Attributes;
using OOH.WebApi.Models.Agency;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace OOH.WebApi.Controllers
{
    public class AgenciesController : Controller
    {
        [OhhFilter("ListAgencies", Data.ActionPermission.Read)]
        public async Task<IActionResult> Index()
        {
            return View();
        }

        #region private methods and functions

        #endregion
    }
}
