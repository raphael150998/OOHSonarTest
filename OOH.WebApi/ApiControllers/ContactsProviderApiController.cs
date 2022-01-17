using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using OOH.Data.Dtos;
using OOH.Data.Models;
using OOH.Data.Repos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace OOH.WebApi.ApiControllers
{
    [ApiController]
    public class ContactsProviderApiController : ControllerBase
    {
        private readonly ContactsProviderRepo _repo;

        public ContactsProviderApiController(ContactsProviderRepo repo)
        {
            _repo = repo;
        }

        [HttpGet]
        [Route("api/provider/contacts/get")]
        public async Task<IActionResult> get(long Id)
        {
            return Ok(await _repo.SelectByProvider(Id));

        }
        [HttpGet]
        [Route("api/provider/contacts/find")]
        public async Task<IActionResult> findContact(int Id)
        {
            return Ok(await _repo.Find(Id));

        }
        [HttpPost]
        [Route("api/provider/contacts/remove")]
        public async Task<IActionResult> delete(Identify<int> data)
        {
            return Ok(await _repo.Remove(data.Id));

        }

        [HttpPost]
        [Route("api/provider/contacts/CEcontact")]
        public async Task<IActionResult> post([FromBody] ProveedoresContactos collection)
        {

            return Ok(await _repo.AddOrUpdate(collection));
        }

    }
}
