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
        public FaceRepository(IWebUserHelper userHelper) : base(userHelper)
        {
        }

        public async Task<ResultClass> AddOrUpdate(Caras collection)
        {
            throw new NotImplementedException();
        }

        public async Task<Caras> Find(int Id)
        {
            throw new NotImplementedException();
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
    }
}
