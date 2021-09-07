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
    public class ClientRepository : OOHContext, IActionRepository<Clientes>
    {
        public string ConectionnString { get; set; }
        public string LogUser { get; set; }
        public ClientRepository(string _stringcontection)
        {
            this.ConectionnString = _stringcontection;            
        } 
        public ClientRepository(string _stringcontection,string _userLoged)
        {
            this.ConectionnString = _stringcontection;
            this.LogUser = _userLoged;
        }
        public async Task<ResultClass> AddOrUpdate(Clientes client)
        {
            if (client.ClienteId == 0)
            {
                if (FilterData<Clientes>($"Select * from [dbo].[clientes] Where Codigo = '{client.Codigo}' ", false, null, this.ConectionnString).Result != null)
                {
                    return new ResultClass() { state = false, message = "El codigo ya existe", data = 1 };
                }
            }
            client.UsuarioId = int.Parse(this.LogUser);
            DynamicParameters param = new DynamicParameters(client);
            
            if (client.ClienteId == 0)
            {
                
                int post = PostData(@"Insert Into [dbo].[clientes](NombreComercial,RazonSocial,PersonaJuridica,Celular,Telefono,Codigo,Direccion,Email,Giro,NIT,NRC,Activo,UsuarioId,CategoriaId,MunicipioId) Values (@NombreComercial,@RazonSocial,@PersonaJuridica,@Celular,@Telefono,@Codigo,@Direccion,@Email,@Giro,@NIT,@NRC,@Activo,@UsuarioId,@CategoriaId,@MunicipioId)", true, param, false, ConectionnString).Result;
               
                return new ResultClass() { data = post, state = post != 0 ?true:false, message = post != 0? "Exito":"No se a podido guardar" };
            }
            else
            {
               
                int post = PostData("update [dbo].[Clientes] set Codigo = @Codigo ,RazonSocial = @RazonSocial ,NombreComercial = @NombreComercial, NRC = @NRC ,NIT= @NIT ,Giro = @Giro ,Email = @Email,Direccion = @Direccion,Telefono = @Telefono ,Celular = @Celular ,PersonaJuridica= @PersonaJuridica, CategoriaId = @CategoriaId, MunicipioId = @MunicipioId Where ClienteId = @ClienteId ", true, param, false, ConectionnString).Result;
                return new ResultClass() { data = post, state = post != 0 ? true : false, message = post != 0 ? "Exito" : "No se a podido guardar" };
            }
        }

        public async Task<Clientes> Find(int Id)
        {
            return FilterData<Clientes>($"Select * from [dbo].[clientes] Where ClienteId = '{Id}'",false,null,ConectionnString).Result;

        }

        public async Task<bool> Remove(int id)
        {
           return PostData($"Update [dbo].[clientes] set Activo = false where ClienteId = '{id}'").Result != 0 ? true : false; 

        }

        public async Task<IEnumerable<Clientes>> Select(string _Where = "")
        {
           return SelectData<Clientes>("select * from [dbo].[clientes] "+_Where,false,null,ConectionnString).Result.ToList();
        }

       
    }
}
