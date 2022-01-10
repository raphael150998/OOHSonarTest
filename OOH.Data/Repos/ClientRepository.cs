using Dapper;
using OOH.Data.Dtos;
using OOH.Data.Dtos.Logs;
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
    public class ClientRepository : AddressRepository
    {
        private readonly ILogHelper _log;
        public ClientRepository(IWebUserHelper userHelper, ILogHelper log) : base(userHelper)
        {
            _log = log;
        }

        public async Task<ResultClass> AddOrUpdate(Clientes client)
        {
            ResultClass result = new ResultClass();

            Clientes validart = FilterData<Clientes>($"Select * from [dbo].[clientes] Where Codigo = '{client.Codigo}'", false, null).Result;
            if (validart != null)
            {
                if (validart.ClienteId != client.ClienteId)
                {
                    return new ResultClass() { state = false, message = "El codigo ya existe", data = 1 };
                }
            }

            client.UsuarioId = _userHelper.GetUserId();

            DynamicParameters param = new DynamicParameters(client);

            if (client.ClienteId == 0)
            {

                int post = PostData(@"Insert Into [dbo].[clientes](NombreComercial,RazonSocial,PersonaJuridica,Celular,Telefono,Codigo,Direccion,Email,Giro,NIT,NRC,Activo,UsuarioId,CategoriaId,MunicipioId) Values (@NombreComercial,@RazonSocial,@PersonaJuridica,@Celular,@Telefono,@Codigo,@Direccion,@Email,@Giro,@NIT,@NRC,@Activo,@UsuarioId,@CategoriaId,@MunicipioId)", true, param, false).Result;

                result = new ResultClass() { data = post, state = post != 0 ? true : false, message = post != 0 ? "Exito" : "No se a podido guardar" };
            }
            else
            {

                int post = UpdateData("update [dbo].[Clientes] set Codigo = @Codigo ,RazonSocial = @RazonSocial ,NombreComercial = @NombreComercial, NRC = @NRC ,NIT= @NIT ,Giro = @Giro ,Email = @Email,Direccion = @Direccion,Telefono = @Telefono ,Celular = @Celular ,PersonaJuridica= @PersonaJuridica, CategoriaId = @CategoriaId, MunicipioId = @MunicipioId Where ClienteId = @ClienteId ", true, param, false).Result;
                result = new ResultClass() { data = post, state = post != 0 ? true : false, message = post != 0 ? "Exito" : "No se a podido guardar" };
            }

            await _log.AddLog(new LogDto()
            {
                Descripcion = client.ClienteId == 0 ? "Creación" : "Actualización",
                Entidad = nameof(Clientes),
                EntidadId = client.ClienteId == 0 ? (int)result.data : client.ClienteId,
            });

            return result;
        }

        public async Task<Clientes> Find(int Id)
        {
            Clientes objeto =  FilterData<Clientes>($"Select * from [dbo].[clientes] Where ClienteId = '{Id}'", false, null).Result;
            objeto.DepartamentoId = GetDepartamentoByMunicipioId(objeto.MunicipioId).Result.DepartamentoId;
            return objeto;
        }

        public async Task<IEnumerable<LogOutputDto>> GetLogs(int id)
        {
            return await _log.GetLogs(new LogInputDto(id, nameof(Clientes)));
        }

        public async Task<bool> Remove(int id)
        {
            await _log.AddLog(new LogDto()
            {
                Descripcion = "Eliminación",
                Entidad = nameof(Clientes),
                EntidadId = id
            });

            return RemoveData($"delete from  [dbo].[clientes] Where ClienteId = {id}", false).Result == 1 ? true : false;

        }

       
        public async Task<IEnumerable<Clientes>> Select()
        {
            return await SelectData<Clientes>("select * from [dbo].[clientes]");
        }
    }
}
