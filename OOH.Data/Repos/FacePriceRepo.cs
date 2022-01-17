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
    public class FacePriceRepo : OOHContext
    {
        private readonly ILogHelper _log;
        public FacePriceRepo(IWebUserHelper userHelper, ILogHelper log) : base(userHelper)
        {
            _log = log;
        }

        public async Task<IEnumerable<TiposPrecios>> GetType()
        {
            return await SelectData<TiposPrecios>("Select * from TiposPrecios");
        }
        public async Task<IEnumerable<FacePriceDto>> GetPriceByFace(long id)
        {
            return await SelectData<FacePriceDto>($"Select t1.Id, t1.TipoId, (Select t2.Nombre From TiposPrecios t2 where t2.Id = t1.TipoId) as Tipo, t1.Precio from CarasPrecios t1 where t1.CaraId ={id}");

        }
        public async Task<FacePriceDto> findbyid(long id)
        {
            return await FilterData<FacePriceDto>($"Select t1.Id, t1.TipoId, (Select t2.Nombre From TiposPrecios t2 where t2.Id = t1.TipoId) as Tipo, t1.Precio from CarasPrecios t1 where t1.Id ={id}");

        }
        public Task<IEnumerable<LogOutputDto>> GetLogs(int id)
        {
            return _log.GetLogs(new LogInputDto(id, nameof(CarasPrecios)));
        }
          
        public async Task<bool> RemovePrice(long id)
        {
            await _log.AddLog(new LogDto()
            {
                Descripcion = "Eliminación",
                Entidad = nameof(CarasPrecios),
                EntidadId = id
            });
            return await RemoveData($"delete from CarasPrecios where id= {id}") == 1 ? true : false;

        }

        public async Task<ResultClass> AddOrUpdate(CarasPrecios collection)
        {
            try
            {
                long save = collection.Id != 0 ? await UpdateData("update CarasPrecios set Precio = @Precio , TipoId = @TipoId where Id = @Id",true,new(collection)) : await PostData("insert into CarasPrecios(TipoId,CaraId,Precio) values (@TipoId,@CaraId,@Precio)", true, new(collection));
                await _log.AddLog(new LogDto()
                {
                    Descripcion = collection.Id == 0 ? "Creación" : "Actualización",
                    Entidad = nameof(CarasPrecios),
                    EntidadId = collection.Id == 0 ? (int)save : collection.Id,
                });

                return new ResultClass() { data = save, message = "Logrado" };
            }
            catch (Exception ex)
            {
                return new ResultClass() { message = "Error" , data = 0 , state = false};
            }
        }
    }
}
