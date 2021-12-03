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
    public class SiteElectricMeterRepository : OOHContext
    {
        private readonly ILogHelper _log;

        public SiteElectricMeterRepository(IWebUserHelper userHelper, ILogHelper log) : base(userHelper)
        {
            _log = log;
        }

        public async Task<ResultClass> AddOrUpdate(SitiosContadorElectrico model)
        {
            ResultClass result = new ResultClass();

            string sql = model.Id == 0 ? "INSERT INTO SitiosContadorElectrico(SitioId, ProveedorId, Active, Porcentaje, ContadorElectrico, NIC) VALUES (@SitioId, @ProveedorId, @Active, @Porcentaje, @ContadorElectrico, @NIC);" : "UPDATE SitiosContadorElectrico SET SitioId = @SitioId, ProveedorId = @ProveedorId, Active = @Active, Porcentaje = @Porcentaje, ContadorElectrico = @ContadorElectrico, NIC = @NIC WHERE Id = @Id;";

            result.data = model.Id == 0 ? await PostData(sql, true, new DynamicParameters(model)) : await UpdateData(sql, true, new DynamicParameters(model));

            result.state = (int)result.data > 0;

            SitiosContadorElectrico oldVwersion = new();

            if (model.Id > 0)
            {
                oldVwersion = await Find(model.Id);
            }

            await _log.AddLog(new LogDto()
            {
                Descripcion = model.Id == 0 ? "Creación" : $"Actualización",
                Entidad = nameof(SitiosContadorElectrico),
                EntidadId = model.Id == 0 ? (int)result.data : model.Id,
                OldVersionJson = model.Id == 0 ? "" : $"{JsonConvert.SerializeObject(oldVwersion)}",
            });

            return result;
        }

        public async Task<SitiosContadorElectrico> Find(long id)
        {
            return await FilterData<SitiosContadorElectrico>($"SELECT * FROM SitiosContadorElectrico WHERE Id = {id}");
        }

        public async Task<SitiosContadorElectrico> FindBySitioId(long sitioId)
        {
            return await FilterData<SitiosContadorElectrico>($"SELECT * FROM SitiosContadorElectrico WHERE SitioId = {sitioId}");
        }

        public async Task<IEnumerable<LogOutputDto>> GetLogs(long id)
        {
            return await _log.GetLogs(new LogInputDto(id, nameof(SitiosContadorElectrico)));
        }

        public async Task<bool> Remove(long id)
        {
            await _log.AddLog(new LogDto()
            {
                Descripcion = "Eliminación",
                Entidad = nameof(SitiosContadorElectrico),
                EntidadId = id
            });

            return await (RemoveData($"DELETE FROM SitiosContadorElectrico WHERE Id = {id}")) > 0;
        }

        public async Task<IEnumerable<SitiosContadorElectrico>> Select()
        {
            return await SelectData<SitiosContadorElectrico>("SELECT * FROM SitiosContadorElectrico");
        }
    }
}
