using Dapper;
using OOH.Data.Dtos;
using OOH.Data.Dtos.Cotizacion;
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
    public class FaceRepository :OOHContext, IBaseRepository<Caras>
    {
        private readonly ILogHelper _log;
        public FaceRepository(IWebUserHelper userHelper, ILogHelper log) : base(userHelper)
        {
            _log = log;
        }

        public async Task<ResultClass> AddOrUpdate(Caras collection)
        {
            ResultClass result = new ResultClass();

            Caras validart = FilterData<Caras>($"Select * from [dbo].[clientes] Where Codigo = '{collection.Codigo}'", false, null).Result;
            if (validart != null)
            {
                if (validart.CaraId != collection.CaraId)
                {
                    return new ResultClass() { state = false, message = "El codigo ya existe", data = 1 };
                }
            }



            DynamicParameters param = new DynamicParameters(collection);

            if (collection.CaraId == 0)
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
                Descripcion = collection.CaraId == 0 ? "Creación" : "Actualización",
                Entidad = nameof(Clientes),
                EntidadId = collection.CaraId == 0 ? (int)result.data : collection.CaraId,
            });

            return result;
        }

        public async Task<Caras> Find(int Id)
        {
            return await FilterData<Caras>($"SELECT  t1.ReferenciaComercial as ReferenciaComercial,   t1.Codigo as Codigo, (select t2.Direccion From [dbo].[Sitios] t2 where t2.SitioId =  t1.SitioId ) as direccion FROM [dbo].[Caras] t1  WHERE t1.CaraId =  {Id}");
        }

        public Task<IEnumerable<LogOutputDto>> GetLogs(int id)
        {
            throw new NotImplementedException();
        }

        public async Task<bool> Remove(int id)
        {
            throw new NotImplementedException();
        }

        public async Task<IEnumerable<FaceQuotationDto>> SelectFace(string _Where = "")
        {
            return SelectData<FaceQuotationDto>("select t1.CaraId,t1.Codigo,t1.ReferenciaComercial,t1.TipoId,(select t2.Nombre from CarasTipos t2 where  t2.TipoId =  t1.TipoId) as tipo,t1.CategoriaId,(select t3.Nombre from CarasCategorias t3 where t3.CategoriaId = t1.CategoriaId ) as categoria from [dbo].[Caras] t1  " + _Where).Result.ToList();   
        }
        public async Task<FaceQuotationDto> FindFace(int Id)
        {
            return await FilterData<FaceQuotationDto>($"SELECT  t1.ReferenciaComercial as ReferenciaComercial,   t1.Codigo as Codigo, ( select t2.Direccion From [dbo].[Sitios] t2 where t2.SitioId =  t1.SitioId ) as direccion FROM [dbo].[Caras] t1  WHERE t1.CaraId =  {Id}");
        }

        public async Task<IEnumerable<Caras>> Select()
        {
            return await SelectData<Caras>("SELECT t1.CaraId, ( select t2.Direccion From [dbo].[Sitios] t2 where t2.SitioId =  t1.SitioId ) as direccion ,t1.Codigo , from [dbo].[Caras] t1");
        }
    }
}
