using OOH.Data.Dtos;
using OOH.Data.Dtos.Cotizacion;
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
        public FaceRepository(IWebUserHelper userHelper) : base(userHelper)
        {
        }

        public async Task<ResultClass> AddOrUpdate(Caras collection)
        {
            throw new NotImplementedException();
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

        public async Task<IEnumerable<Caras>> Select(string _Where = "")
        {
            throw new NotImplementedException();
        }
        public async Task<IEnumerable<FaceQuotationDto>> SelectFace(string _Where = "")
        {
            return SelectData<FaceQuotationDto>("select t1.CaraId,t1.Codigo,t1.ReferenciaComercial,t1.TipoId,(select t2.Nombre from CarasTipos t2 where  t2.TipoId =  t1.TipoId) as tipo,t1.CategoriaId,(select t3.Nombre from CarasCategorias t3 where t3.CategoriaId = t1.CategoriaId ) as categoria from [dbo].[Caras] t1  " + _Where).Result.ToList();   
        }
        public async Task<FaceQuotationDto> FindFace(int Id)
        {
            return await FilterData<FaceQuotationDto>($"SELECT  t1.ReferenciaComercial as ReferenciaComercial,   t1.Codigo as Codigo, (select t2.Direccion From [dbo].[Sitios] t2 where t2.SitioId =  t1.SitioId ) as direccion FROM [dbo].[Caras] t1  WHERE t1.CaraId =  {Id}");
        }
    }
}
