﻿using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using OOH.Data.Dtos;
using OOH.Data.Dtos.Site;
using OOH.Data.Helpers;
using OOH.Data.Models;
using OOH.Data.Repos;
using OOH.WebApi.Filters.Attributes;
using OOH.WebApi.Models.Site;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace OOH.WebApi.ApiControllers
{
    [Route("api/site")]
    [ApiController]
    public class SitesApiController : ControllerBase
    {
        private readonly SiteRepository _siteRepo;
        private readonly AddressRepository _addressRepo;
        private readonly SiteElectricMeterRepository _electricityMeterRepo;
        private readonly IMapper _mapper;


        public SitesApiController(SiteRepository repo, IMapper mapper, SiteElectricMeterRepository electricityMeterRepo, AddressRepository addressRepo)
        {
            _siteRepo = repo;
            _mapper = mapper;
            _electricityMeterRepo = electricityMeterRepo;
            _addressRepo = addressRepo;
        }

        [HttpGet("select")]
        [OhhFilterAttribute("ListSites", Data.ActionPermission.Read)]
        public async Task<IActionResult> GetList()
        {
            return Ok(await _siteRepo.GetList());
        }

        [HttpGet("log")]
        [OhhFilterAttribute("Sites", Data.ActionPermission.Execute)]
        public async Task<IActionResult> GetLogs(int id)
        {
            return Ok(await _siteRepo.GetLogs(id));
        }

        [HttpGet("find")]
        [OhhFilterAttribute("Sites", Data.ActionPermission.Read)]
        public async Task<IActionResult> Find(int id)
        {
            Sitios site = await _siteRepo.Find(id);

            if (site == null) return NotFound();

            SitiosContadorElectrico electricityInfo = await _electricityMeterRepo.FindBySitioId(site.SitioId);

            SiteVm siteVm = _mapper.Map<SiteVm>(site);
            siteVm.DepartamentoId = (await _addressRepo.GetMunicipioById(siteVm.MunicipioId)).DepartamentoId;

            if (electricityInfo != null)
            {
                siteVm.ProveedorElectricidadId = electricityInfo.ProveedorId;
                siteVm.Porcentaje = electricityInfo.Porcentaje;
                siteVm.ContadorElectrico = electricityInfo.ContadorElectrico;
                siteVm.NIC = electricityInfo.NIC;
            }

            return Ok(siteVm);
        }

        [HttpPost("select2")]
        [OhhFilterAttribute("ListSites", Data.ActionPermission.Read)]
        public async Task<IActionResult> GetListAsSelect2([FromBody] SiteSelect2InputDto model)
        {
            List<string> keys = string.IsNullOrEmpty(model.term) ? new() : model.term.Split(' ').ToList();
            return Ok(await _siteRepo.GetListForSelect2(new Select2PagingInputDto()
            {
                Search = keys,
                CurrentPage = model.page
            }));
        }

        [HttpPost("CreateUpdate")]
        [OhhFilterAttribute("Sites", Data.ActionPermission.Create)]
        [OhhFilterAttribute("Sites", Data.ActionPermission.Update)]
        public async Task<IActionResult> CreateUpdate([FromBody] SiteVm model)
        {
            ResultClass response = new ResultClass();


            try
            {
                Sitios site = _mapper.Map<Sitios>(model);

                response = await _siteRepo.AddOrUpdate(site);

                SitiosContadorElectrico electricityInfo = await _electricityMeterRepo.FindBySitioId(site.SitioId);

                electricityInfo = model.SitioId == 0 || electricityInfo == null ? new() : electricityInfo;

                electricityInfo.SitioId = model.SitioId;
                electricityInfo.ProveedorId = model.ProveedorElectricidadId;
                electricityInfo.Porcentaje = model.Porcentaje;
                electricityInfo.ContadorElectrico = model.ContadorElectrico;
                electricityInfo.NIC = model.NIC;

                await _electricityMeterRepo.AddOrUpdate(electricityInfo);

            }
            catch (Exception ex)
            {
                response.message = ex.Message;
                response.state = false;
            }

            return Ok(response);
        }

        [HttpPost("Remove")]
        [OhhFilterAttribute("Sites", Data.ActionPermission.Delete)]
        public async Task<IActionResult> Remove([FromBody] Identify<int> obj)
        {
            return Ok(await _siteRepo.Remove(obj.Id));
        }
    }
}
