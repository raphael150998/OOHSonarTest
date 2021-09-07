using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using OOH.Data.Helpers;
using OOH.Data.Interfaces;
using OOH.Data.Models;
using OOH.WebApi.Models.Agency;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace OOH.WebApi.Controllers
{
    public class AgenciesController : Controller
    {
        private readonly IAdvertisingAgencyRepository _repo;
        private readonly IMapper _mapper;

        public AgenciesController(IAdvertisingAgencyRepository repo, IMapper mapper)
        {
            _repo = repo;
            _mapper = mapper;
        }

        public async Task<IActionResult> Index()
        {
            return View();
        }

        [HttpGet]
        public async Task<IActionResult> CreateUpdate(int id = 0)
        {
            AgencyVm agency = id == 0 ? new AgencyVm() : _mapper.Map<AgencyVm>(await _repo.Find(id));
            return View(agency);
        }

        #region private methods and functions

        #endregion
    }
}
