using OOH.Data.Dtos.Caras;
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
    public class CaraMaterialRepository : OOHContext
    {
        public CaraMaterialRepository(IWebUserHelper userHelper) : base(userHelper)
        {
        }

        public async Task<ResultClass> AddOrUpdate(CarasMateriales model)
        {
            try
            {
                model.Id = 0;
                return new ResultClass() { data = PostData("insert into CarasMateriales(CaraId,MaterialId) values(@CaraId,@MaterialId) ", true,new(model)) };
            }
            catch (Exception ex)
            {

                return new ResultClass() { data = null };
            }

        }


        public async Task<bool> Remove(long id)
        {
            return await RemoveData($"Delete from CarasMateriales where Id = {id}") == 1 ? true : false;
        }

        public Task<IEnumerable<FaceMaterialDto>> Select(long id)
        {
            return SelectData<FaceMaterialDto>($"Select t1.Id  , (select t2.Codigo from Materiales t2 where t2.MaterialId = t1.MaterialId) as Codigo , (select t2.mateNombre from Materiales t2 where t2.MaterialId = t1.MaterialId) as Material ,  (select t3.Costo from Materiales t3 where t3.MaterialId = t1.MaterialId) as Costo from CarasMateriales t1 where t1.CaraId = {id}");
        }
       
    }
}
