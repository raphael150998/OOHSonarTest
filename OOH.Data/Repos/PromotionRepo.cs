using Dapper;
using Newtonsoft.Json;
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
    public class PromotionRepo : OOHContext
    {
        private readonly ILogHelper _log;

        public PromotionRepo(IWebUserHelper userHelper, ILogHelper log) : base(userHelper)
        {
            _log = log;
        }

        public async Task<ResultClass> AddOrUpdate(Promociones model)
        {
            ResultClass result = new ResultClass();

            string sql = model.PromocionId == 0 ? "INSERT INTO Promociones(Descripcion, Activo) VALUES (@Descripcion, @Activo);" : "UPDATE Promociones SET Descripcion = @Descripcion, Activo = @Activo WHERE PromocionId = @PromocionId;";

            result.data = model.PromocionId == 0 ? await PostData(sql, true, new DynamicParameters(model)) : await UpdateData(sql, true, new DynamicParameters(model));

            result.state = (int)result.data > 0;

            Promociones oldVwersion = new();

            if (model.PromocionId > 0)
            {
                oldVwersion = await Find(model.PromocionId);
            }

            await _log.AddLog(new LogDto()
            {
                Descripcion = model.PromocionId == 0 ? "Creación" : $"Actualización",
                Entidad = nameof(Promociones),
                EntidadId = model.PromocionId == 0 ? (int)result.data : model.PromocionId,
                OldVersionJson = model.PromocionId == 0 ? "" : $"{JsonConvert.SerializeObject(oldVwersion)}",
            });

            return result;
        }

        public async Task<Promociones> Find(long id)
        {
            return await FilterData<Promociones>($"SELECT * FROM Promociones WHERE PromocionId = {id}");
        }

        public async Task<IEnumerable<LogOutputDto>> GetLogs(long id)
        {
            return await _log.GetLogs(new LogInputDto(id, nameof(Promociones)));
        }

        public async Task<bool> Remove(long id)
        {
            await _log.AddLog(new LogDto()
            {
                Descripcion = "Eliminación",
                Entidad = nameof(Promociones),
                EntidadId = id
            });

            return await (RemoveData($"DELETE FROM Promociones WHERE PromocionId = {id}")) > 0;
        }

        public async Task<IEnumerable<Promociones>> Select()
        {
            return await SelectData<Promociones>("SELECT * FROM Promociones");
        }
    }
}
