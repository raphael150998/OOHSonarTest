using Dapper;
using OOH.Data.Interfaces;
using OOH.Data.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OOH.Data.Repos
{
    public class ClientRepository : OOHContext, IClientRepository
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
        public async Task<int> AddOrUpdate(Clientes client)
        {
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
            param.Add("@Activo", client.Activo);
            if (client.ClienteId == 0)
            {
                param.Add("@Usuario", this.LogUser);
                return PostData(@"Insert Into [dbo].[clientes](ClienteId,NombreComercial,RazonSocial,PersonaJuridica,Celular,Telefono,Codigo,Direccion,Email,Giro,NIT,NRC,Activo,UsuarioId) Values (@id,@NombreC,@Razon,@Pjuridica,@Celular,@Telefono,@Codigo,@Direccion,@Email,@Giro,@NIT,@NRC,@Activo,@Usuario)", true, param, false, ConectionnString).Result;
            }
            else
            {
                param.Add("@Usuario", client.UsuarioId);
                return PostData("Update [dbo].[clientes] set",true,param,false,ConectionnString).Result;
            }
        }

        public async Task<Clientes> Find(int Id)
        {
            return FilterData<Clientes>($"Select * from [dbo].[clientes] Where ClienteId = {Id}",false,null,ConectionnString).Result;

        }

        public async Task Remove(int id)
        {
            await PostData($"Update [dbo].[clientes] set Activo = false where ClienteId = {id}");

        }

        public async Task<IEnumerable<Clientes>> Select(string _Where = "")
        {
           return SelectData<Clientes>("select * from [dbo].[clientes] "+_Where,false,null,ConectionnString).Result.ToList();
        }

       
    }
}
