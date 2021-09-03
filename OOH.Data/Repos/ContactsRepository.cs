using Dapper;
using OOH.Data.Helpers;
using OOH.Data.Interfaces;
using OOH.Data.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OOH.Data.Repos
{
    public class ContactsRepository : OOHContext, IActionRepository<ClientesContactos>
    {
        public string ConectionnString { get; set; }
        public ContactsRepository(string _stringcontection)
        {
            this.ConectionnString = _stringcontection;
        }
        public async Task<ResultClass> AddOrUpdate(ClientesContactos collection)
        {

            DynamicParameters param = new DynamicParameters();
            param.Add("@id", collection.Id);
            param.Add("@Cliente", collection.ClienteId);
            param.Add("@Nombres", collection.Nombres);
            param.Add("@Apellidos", collection.Apellidos);
            param.Add("@Rol", collection.Rol);
            param.Add("@Email", collection.Email);
            param.Add("@Telefono", collection.Telefono);
            param.Add("@Celular", collection.Celular);
            if (collection.Id == 0)
            {
                return new ResultClass { data = PostData(@"Insert [dbo].[ClientesContactos] (Id,ClienteId,Nombres,Celular,Apellidos,Rol,Telefono,Email) Values(@id,@Cliente,@Nombres,@Celular,@Apellidos,@Rol,@Telefono,@Email)", true, param, false, ConectionnString).Result };
            }
            else
            {
                return new ResultClass{data= PostData("Update [dbo].[clientes] set", true, param, false, ConectionnString).Result};
            }
        }

        public async Task<ClientesContactos> Find(int Id)
        {
            return FilterData<ClientesContactos>($"Select * from [dbo].[ClientesContactos] Where Id = {Id}", false, null, ConectionnString).Result;

        }

        public Task<bool> Remove(int id)
        {
            throw new NotImplementedException();
        }

        public async Task<IEnumerable<ClientesContactos>> Select(string _Where = "")
        {
            return SelectData<ClientesContactos>($"Select * from [dbo].[ClientesContactos]"+ _Where).Result.ToList();
        }
    }
}
