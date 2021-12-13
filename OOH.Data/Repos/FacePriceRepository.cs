using OOH.Data.Dtos.Caras;
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
    public class FacePriceRepository : OOHContext
    {
        public FacePriceRepository(IWebUserHelper userHelper) : base(userHelper)
        {
        }

        public async Task<IEnumerable<TiposPrecios>> GetType()
        {
            return await SelectData<TiposPrecios>("Select * from TiposPrecios");
        }
        public async Task<IEnumerable<FacePriceDto>> GetPriceByFace(long id)
        {
            return await SelectData<FacePriceDto>($"Select t1.Id, t1.TipoId, (Select t2.Nombre From TiposPrecios t2 where t2.Id = t1.TipoId) as Tipo, t1.Precio from CarasPrecios t1 where t1.CaraId ={id}");

        }

        public async Task<bool> RemovePrice(long id)
        {
            return await RemoveData($"delete from CarasPrecios where id= {id}") == 1 ? true : false;

        }

        public async Task<ResultClass> AddOrUpdate(CarasPrecios collection)
        {
            try
            {
                return new ResultClass() { data = await PostData("insert into CarasPrecios(TipoId,CaraId,Precio) values (@TipoId,@CaraId,@Precio)",true,new(collection)), message = "Logrado" };
            }
            catch (Exception ex)
            {
                return new ResultClass() { message = "Error" , data = 0 , state = false};
            }
        }
    }
}
