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
        ContactsRepository _repos;
        [HttpGet]
        [Route("api/contacts/list")]
        public async Task<List<ClientesContactos>> Contactos(int clientId)
        {
            _repos = new ContactsRepository(txtConectionString());
            return _repos.Select($"Where ClienteId = {clientId}").Result.ToList();
        }

        [HttpPost]
        [Route("api/contacts/CEcontact")]
        public async Task<ResultClass> CreateEdit([FromBody] ClientesContactos contacto)
        {
            _repos = new ContactsRepository(txtConectionString());
            return _repos.AddOrUpdate(contacto).Result;
        }


        [HttpGet]
        [Route("api/contacts/contact")]
        public async Task<ClientesContactos> FindContact(int id)
        {

            _repos = new ContactsRepository(txtConectionString());
            return _repos.Select($"Where Id = {id}").Result.FirstOrDefault();


        }

    }
}
