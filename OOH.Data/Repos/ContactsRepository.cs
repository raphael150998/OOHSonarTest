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
    public class ContactsRepository : OOHContext, IBaseRepository<ClientesContactos>
    {
        public ContactsRepository(IWebUserHelper userHelper) : base(userHelper)
        {
        }

      
        public async Task<ResultClass> AddOrUpdate(ClientesContactos collection)
        {

            DynamicParameters param = new DynamicParameters();
            param.Add("@Cliente", collection.ClienteId);
            param.Add("@Nombres", collection.Nombres);
            param.Add("@Apellidos", collection.Apellidos);
            param.Add("@Rol", collection.Rol);
            param.Add("@Email", collection.Email);
            param.Add("@Telefono", collection.Telefono);
            param.Add("@Celular", collection.Celular);
            if (collection.Id == 0)
            {
                return new ResultClass { data = PostData(@"Insert [dbo].[ClientesContactos] (ClienteId,Nombres,Celular,Apellidos,Rol,Telefono,Email) Values(@Cliente,@Nombres,@Celular,@Apellidos,@Rol,@Telefono,@Email)", true, param, false).Result };
            }
            else
            {
                param.Add("@id", collection.Id);
                return new ResultClass{data= PostData(" Update [dbo].[ClientesContactos] set [ClienteId] = @Cliente , Nombres = @Nombres, Apellidos = @Apellidos , Rol = @Rol , Email = @Email, Telefono = @Telefono ,Celular = @Celular Where Id = @id", true, param, false).Result};
            }
        }

        public async Task<ClientesContactos> Find(int Id)
        {
            return FilterData<ClientesContactos>($"Select * from [dbo].[ClientesContactos] Where Id = {Id}", false, null).Result;

        }

        public Task<bool> Remove(int id)
        {
            throw new NotImplementedException();
        }

        public async Task<IEnumerable<ClientesContactos>> Select(string _Where = "")
        {
            return SelectData<ClientesContactos>($"Select * from [dbo].[ClientesContactos] "+ _Where,false,null).Result.ToList();
        }
    }
}
