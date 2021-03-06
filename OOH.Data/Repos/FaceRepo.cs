using Dapper;
using OOH.Data.Dtos;
using OOH.Data.Dtos.Caras;
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
    public class FaceRepo : OOHContext, IBaseRepo<Caras>
    {
        private readonly ILogHelper _log;
        public FaceRepo(IWebUserHelper userHelper, ILogHelper log) : base(userHelper)
        {
            _log = log;
        }

        public async Task<int> AddSaliente(CaraSalientes saliente)
        {
            try
            {
                return await PostData("insert into CaraSalientes(CaraId,SalienteId) values(@CaraId,@SalienteId)", true, new(saliente));
            }
            catch (Exception ex)
            {

                return 0;
            }
        }
        public async Task<bool> RemoveSaliente(long saliente)
        {
            try
            {
                await RemoveData($"delete from CaraSalientes Where Id = {saliente}");
                return true;
            }
            catch (Exception ex)
            {

                return false;
            }
        }

        public async Task<IEnumerable<CaraSalientes>> FindSaliente(long id)
        {
            try
            {
                return await SelectData<CaraSalientes>($"Select * From CaraSalientes Where CaraId = {id}");
            }
            catch (Exception ex)
            {

                return new List<CaraSalientes>();
            }
        }


        public async Task<ResultClass> AddOrUpdate(Caras collection)
        {
            ResultClass result = new ResultClass();
            DynamicParameters param = new DynamicParameters(collection);

            string sitio = await FilterData<string>($"Select Codigo from Sitios where SitioId = {collection.SitioId}");
            string tipo = await FilterData<string>($"Select Codigo from CarasTipos where TipoId = {collection.TipoId}");

            if (collection.CaraId != 0)
            {

                string idSitio = await FilterData<string>($"Select SitioId from Caras where CaraId = {collection.CaraId}");
                if (Convert.ToInt64(idSitio) != collection.SitioId)
                {
                    await UpdateData($"update Caras Set Activo = 0 where CaraId = {collection.CaraId}", false);
                    await _log.AddLog(new LogDto()
                    {
                        Descripcion = collection.CaraId == 0 ? "Creación" : "Actualización",
                        Entidad = nameof(Caras),
                        EntidadId = collection.CaraId == 0 ? (int)result.data : collection.CaraId,
                    });
                    collection.CaraId = 0;
                }
            }



            if (collection.CaraId == 0)
            {
                collection.Codigo = tipo + "-" + sitio + "-" + collection.Lado;
                int post = PostData(@"Insert Into [dbo].[Caras] ([SitioId],[NotaInstalacion],[Codigo],[TipoId],[CategoriaId],[Alto],[Ancho],[Sentido],[AlturaAlPiso],[MetodoInstalacion],[Observaciones],[Activo],[ReferenciaComercial],[NumSpotDigital],[CaraIluminada]) 
                                                        Values (@SitioId,@NotaInstalacion,@Codigo,@TipoId,@CategoriaId,@Alto,@Ancho,@Sentido,@AlturaAlPiso,@MetodoInstalacion,@Observaciones,@Activo,@ReferenciaComercial,@NumSpotDigital,@CaraIluminada)", true, param, false).Result;

                result = new ResultClass() { data = post, state = post != 0 ? true : false, message = post != 0 ? "Exito" : "No se a podido guardar" };
            }
            else
            {
                collection.Codigo = tipo + "-" + sitio + "-" + collection.Sentido;
                int post = UpdateData(@"update [dbo].[Caras] set SitioId = @SitioId ,Codigo = @Codigo ,TipoId = @TipoId ,CategoriaId= @CategoriaId ,Alto = @Alto ,
                                                 Ancho = @Ancho,Sentido = @Sentido,AlturaAlPiso = @AlturaAlPiso ,MetodoInstalacion = @MetodoInstalacion ,Observaciones= @Observaciones,  
                                                 Activo = @Activo, ReferenciaComercial = @ReferenciaComercial, NumSpotDigital = @NumSpotDigital, CaraIluminada = @CaraIluminada Where CaraId = @CaraId ", true, param, false).Result;
                result = new ResultClass() { data = post != 0 ? collection.CaraId : 0, state = post != 0 ? true : false, message = post != 0 ? "Exito" : "No se a podido guardar" };
            }

            await _log.AddLog(new LogDto()
            {
                Descripcion = collection.CaraId == 0 ? "Creación" : "Actualización",
                Entidad = nameof(Caras),
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
            return _log.GetLogs(new LogInputDto(id, nameof(Caras)));
        }

        public async Task<bool> Remove(int id)
        {
            await _log.AddLog(new LogDto()
            {
                Descripcion = "Eliminación",
                Entidad = nameof(Caras),
                EntidadId = id
            });

            return RemoveData($"delete from  [dbo].[Caras] Where CaraId = {id}", false).Result == 1 ? true : false;
        }

        public async Task<IEnumerable<Caras>> Select(string _Where = "")
        {
            return await SelectData<Caras>("SELECT t1.CaraId, ( select t2.Direccion From [dbo].[Sitios] t2 where t2.SitioId =  t1.SitioId ) as direccion ,t1.Codigo , from [dbo].[Caras] t1");
        }
        public async Task<IEnumerable<FaceDto>> SelectCaras()
        {
            return await SelectData<FaceDto>("SELECT t1.CaraId, t1.SitioId ,( select t2.Codigo From [dbo].[Sitios] t2 where t2.SitioId =  t1.SitioId ) as sitio ,t1.Codigo ,(select t3.Nombre from CarasTipos t3 where  t3.TipoId =  t1.TipoId) as tipo, TipoId,(select t4.Nombre from Municipios t4 Where t4.MunicipioId = (select t5.MunicipioId From [dbo].[Sitios] t5 where t5.SitioId =  t1.SitioId) ) as municipio,t1.Activo as activa, t1.ReferenciaComercial as referencia, t1.CaraIluminada as iluminada from [dbo].[Caras] t1");
        }

        public async Task<Caras> FilterFace(int id)
        {
            return await FilterData<Caras>($"select * from [dbo].[Caras] where CaraId = {id}");
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
