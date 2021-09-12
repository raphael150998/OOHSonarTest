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

            DynamicParameters param = new DynamicParameters(collection);
         
            if (collection.Id == 0)
            {
                try
                {

                    return new ResultClass { data = PostData(@"Insert into [dbo].[ClientesContactos] (ClienteId,Nombres,Celular,Apellidos,RolId,Telefono,Email) Values(@ClienteId,@Nombres,@Celular,@Apellidos,@RolId,@Telefono,@Email)", true, param, false).Result };
                }
                catch (Exception ex)
                {

                    throw;
                }
            }
            else
            {
               
                return new ResultClass{data= UpdateData(" Update [dbo].[ClientesContactos] set [ClienteId] = @ClienteId , Nombres = @Nombres, Apellidos = @Apellidos , RolId = @RolId , Email = @Email, Telefono = @Telefono ,Celular = @Celular Where Id = @Id", true, param, false).Result};
            }
        }

        public async Task<ClientesContactos> Find(int Id)
        {
            return FilterData<ClientesContactos>($"Select * from [dbo].[ClientesContactos] Where Id = {Id}", false, null).Result;

        }

        public async Task<bool> Remove(int id)
        {
            return RemoveData($"delete from [dbo].[ClientesContactos] Where Id = {id}",false).Result == 1 ? true : false;
        }

        public async Task<IEnumerable<ClientesContactos>> Select(string _Where = "")
        {
            return SelectData<ClientesContactos>($"Select * from [dbo].[ClientesContactos] "+ _Where,false,null).Result.ToList();
        }
        public async Task<IEnumerable<ClientesContactosRoles>> Roles(string _Where = "")
        {
            return SelectData<ClientesContactosRoles>($"Select * from [dbo].[ClientesContactosRoles] " + _Where,false,null).Result.ToList();
        }


    }
}
