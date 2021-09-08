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
    public class ClientRepository :  IBaseRepository<Clientes>
    {
        private readonly OOHContext _db;
        public ClientRepository(OOHContext db)
        {
            _db = db;
        }

        public async Task<ResultClass> AddOrUpdate(Clientes client)
        {
            if (client.ClienteId == 0)
            {
                if (_db.FilterData<Clientes>($"Select * from [dbo].[clientes] Where Codigo = '{client.Codigo}' ", false, null).Result != null)
                {
                    return new ResultClass() { state = false, message = "El codigo ya existe", data = 1 };
                }
            }
            client.UsuarioId = _db._userHelper.GetUserId();
            DynamicParameters param = new DynamicParameters(client);

            if (client.ClienteId == 0)
            {

                int post = _db.PostData(@"Insert Into [dbo].[clientes](NombreComercial,RazonSocial,PersonaJuridica,Celular,Telefono,Codigo,Direccion,Email,Giro,NIT,NRC,Activo,UsuarioId,CategoriaId,MunicipioId) Values (@NombreComercial,@RazonSocial,@PersonaJuridica,@Celular,@Telefono,@Codigo,@Direccion,@Email,@Giro,@NIT,@NRC,@Activo,@UsuarioId,@CategoriaId,@MunicipioId)", true, param, false).Result;

                return new ResultClass() { data = post, state = post != 0 ? true : false, message = post != 0 ? "Exito" : "No se a podido guardar" };
            }
            else
            {

                int post = _db.PostData("update [dbo].[Clientes] set Codigo = @Codigo ,RazonSocial = @RazonSocial ,NombreComercial = @NombreComercial, NRC = @NRC ,NIT= @NIT ,Giro = @Giro ,Email = @Email,Direccion = @Direccion,Telefono = @Telefono ,Celular = @Celular ,PersonaJuridica= @PersonaJuridica, CategoriaId = @CategoriaId, MunicipioId = @MunicipioId Where ClienteId = @ClienteId ", true, param, false).Result;
                return new ResultClass() { data = post, state = post != 0 ? true : false, message = post != 0 ? "Exito" : "No se a podido guardar" };
            }
        }

        public async Task<Clientes> Find(int Id)
        {
            return _db.FilterData<Clientes>($"Select * from [dbo].[clientes] Where ClienteId = '{Id}'", false, null).Result;

        }

        public async Task<bool> Remove(int id)
        {
            return _db.PostData($"Update [dbo].[clientes] set Activo = false where ClienteId = '{id}'").Result != 0 ? true : false;

        }

        public async Task<IEnumerable<Clientes>> Select(string _Where = "")
        {
            return _db.SelectData<Clientes>("select * from [dbo].[clientes] " + _Where, false, null).Result.ToList();
        }


    }
}
