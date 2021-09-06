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

        public async Task<JsonResult> GetList()
        {
            IEnumerable<AgenciasPublicidad> agencies = await _repo.Select();

            return Json(agencies);
        }

        [HttpGet]
        public async Task<IActionResult> CreateUpdate(int id = 0)
        {
            AgencyVm agency = id == 0 ? new AgencyVm() : _mapper.Map<AgencyVm>(await _repo.Find(id));
            return View(agency);
        }

        [HttpPost]
        public async Task<JsonResult> CreateUpdate(AgencyVm model)
        {
            ResultClass response = new ResultClass();

            try
            {
                AgenciasPublicidad agency = await _repo.Find(model.Id);

                agency = agency ?? new AgenciasPublicidad();

                agency.AgenciaId = model.Id;
                agency.Nombre = model.Name;
                agency.Comision = model.Rate;

                int id = 0;

                if (agency.AgenciaId == 0)
                {
                    id = await _repo.Create(agency);
                    response.data = id;
                }
                else
                {
                    await _repo.Update(agency);
                }
            }
            catch (Exception ex)
            {
                response.message = ex.Message;
                response.state = false;
            }

            return Json(response);
        }

        #region private methods and functions

        #endregion
    }
}
