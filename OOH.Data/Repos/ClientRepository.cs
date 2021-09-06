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
            
            DynamicParameters param = new DynamicParameters();
            param.Add("@id", client.ClienteId);
            param.Add("@NombreC", client.NombreComercial);
            param.Add("@Razon", client.RazonSocial);
            param.Add("@Pjuridica", client.PersonaJuridica);
            param.Add("@Celular", client.Celular);
            param.Add("@Telefono", client.Telefono);
            param.Add("@Codigo", client.Codigo);
            param.Add("@Direccion", client.Direccion);
            param.Add("@Email", client.Email);
            param.Add("@Giro", client.Giro);
            param.Add("@NIT", client.NIT);
            param.Add("@NRC", client.NRC);
            param.Add("@CategoriaId", client.CategoriaId);
            param.Add("@Activo", true);
            param.Add("@Municipio", client.MunicipioId);
            if (client.ClienteId == 0)
            {
                param.Add("@Usuario", this.LogUser);
                int post = PostData(@"Insert Into [dbo].[clientes](NombreComercial,RazonSocial,PersonaJuridica,Celular,Telefono,Codigo,Direccion,Email,Giro,NIT,NRC,Activo,UsuarioId,CategoriaId,MunicipioId) Values (@NombreC,@Razon,@Pjuridica,@Celular,@Telefono,@Codigo,@Direccion,@Email,@Giro,@NIT,@NRC,@Activo,@Usuario,@CategoriaId,@Municipio)", true, param, false, ConectionnString).Result;
               
                return new ResultClass() { data = post, state = post != 0 ?true:false, message = post != 0? "Exito":"No se a podido guardar" };
            }
            else
            {
                param.Add("@Usuario", client.UsuarioId);
                int post = PostData("Update [dbo].[clientes] set", true, param, false, ConectionnString).Result;
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
