using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using OOH.Data.Helpers;
using OOH.Data.Models;
using OOH.Data.Repos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace OOH.WebApi.ApiControllers
{

    [ApiController]
    public class ContactsApiController : BaseApiController
    {
        private readonly ContactsRepository _repo;

        public ContactsApiController(ContactsRepository repo)
        {
            _repo = repo;
        }
        [HttpGet]
        [Route("api/contacts/list")]
        public async Task<List<ClientesContactos>> Contactos(int clientId)
        {
            return _repo.Select($"Where ClienteId = {clientId}").Result.ToList();
        }

        [HttpPost]
        [Route("api/contacts/CEcontact")]
        public async Task<ResultClass> CreateEdit([FromBody] ClientesContactos contacto)
        {
            return _repo.AddOrUpdate(contacto).Result;
        }


        [HttpGet]
        [Route("api/contacts/contact")]
        public async Task<ClientesContactos> FindContact(int id)
        {
            return _repo.Select($"Where Id = {id}").Result.FirstOrDefault();


        }

    }
}
