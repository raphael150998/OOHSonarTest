using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using OOH.Data.Dtos;
using OOH.Data.Helpers;
using OOH.Data.Interfaces;
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
        public async Task<List<ClientesContactos>> Contactos(string clientId)
        {
            return _repo.Select($"Where ClienteId = {clientId}").Result.ToList();
        }
        [HttpGet]
        [Route("api/Roles/call")]
        public async Task<List<ClientesContactosRoles>> Roles()
        {
            return _repo.Roles().Result.ToList();
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

        [HttpPost]
        [Route("api/contacts/remove")]
        public async Task<bool> Eliminar([FromBody]Identify<int> obj) {

            return _repo.Remove(obj.Id).Result;
        
        }

    }
}
