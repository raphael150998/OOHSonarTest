using Microsoft.AspNetCore.Mvc;
using OOH.Data.Models;
using OOH.Data.Repos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace OOH.WebApi.ApiControllers
{
    [Route("api/address")]
    [ApiController]
    public class AddressApiController : Controller
    {
        private readonly AddressRepository _repo;

        public AddressApiController(AddressRepository repo)
        {
            _repo = repo;
        }

        [HttpGet("departments")]
        public async Task<IActionResult> GetDeparments()
        {
            return Ok(await _repo.SelectDepartamentos());
        }

        [HttpGet("dropdownDepartments")]
        public async Task<IActionResult> GetListForDepartmentsDropdown()
        {
            List<Departamentos> departments = (await _repo.SelectDepartamentos()).ToList();

            object result = departments.Count() > 0 ? departments.Select(x => new { Id = x.DepartamentoId, Name = x.Nombre }) : new List<object>() { };

            return Ok(result);
        }


        [HttpGet("dropdownCities/{id}")]
        public async Task<IActionResult> GetListForCityByDepartmentDropdown(int id)
        {
            List<Municipios> cities = (await _repo.SelectMunicipiosByDepartamentoId(id)).ToList();

            object result = cities.Count() > 0 ? cities.Select(x => new { Id = x.MunicipioId, Name = x.Nombre }) : new List<object>() { };

            return Ok(result);
        }

        [HttpGet("departmentByCity/{id}")]
        public async Task<IActionResult> GetDepartmentByCity(int id)
        {
            return Ok(await _repo.GetDepartamentoByMunicipioId(id));
        }
    }
}
