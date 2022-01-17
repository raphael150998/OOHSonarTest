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
    public class CaraMaterialRepo : OOHContext
    {
        private readonly ILogHelper _log;
        public CaraMaterialRepo(IWebUserHelper userHelper, ILogHelper log) : base(userHelper)
        {
            _log = log;
        }

        public async Task<ResultClass> AddOrUpdate(CarasMateriales model)
        {
            try
            {
                long save = model.Id ==  0 ? await PostData("insert into CarasMateriales(CaraId,MaterialId) values(@CaraId,@MaterialId) ", true, new(model))
                    : await UpdateData($"update CarasMateriales set MaterialId = @MaterialId where Id = @Id",true,new (model));
                await _log.AddLog(new LogDto()
                {
                    Descripcion = model.Id == 0 ? "Creación" : "Actualización",
                    Entidad = nameof(CarasPrecios),
                    EntidadId = model.Id == 0 ? (int)save : model.Id,
                });
                return new ResultClass() { data = save};
            }
            catch (Exception ex)
            {

                return new ResultClass() { data = null };
            }

        }
        public Task<IEnumerable<LogOutputDto>> GetLogs(int id)
        {
            return _log.GetLogs(new LogInputDto(id, nameof(CarasMateriales)));
        }

        public async Task<bool> Remove(long id)
        {
            await _log.AddLog(new LogDto()
            {
                Descripcion = "Eliminación",
                Entidad = nameof(CarasMateriales),
                EntidadId = id
            });
            return await RemoveData($"Delete from CarasMateriales where Id = {id}") == 1 ? true : false;
        }

        public async Task<CarasMateriales> findbyid(long id)
        {
            return await FilterData<CarasMateriales>($"select * from CarasMateriales t1 where Id = {id}");

        }

        public async Task<IEnumerable<FaceMaterialDto>> Select(long id)
        {
            return  await SelectData<FaceMaterialDto>($"Select t1.Id  , (select t2.Codigo from Materiales t2 where t2.MaterialId = t1.MaterialId) as Codigo , (select t2.mateNombre from Materiales t2 where t2.MaterialId = t1.MaterialId) as Material ,  (select t3.Costo from Materiales t3 where t3.MaterialId = t1.MaterialId) as Costo from CarasMateriales t1 where t1.CaraId = {id}");
        }
       
    }
}
