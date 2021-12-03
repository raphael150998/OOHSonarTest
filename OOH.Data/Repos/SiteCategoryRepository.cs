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
    public class SiteCategoryRepository : OOHContext, IBaseRepository<SitiosCategorias>
    {
        private readonly ILogHelper _log;

        public SiteCategoryRepository(IWebUserHelper userHelper, ILogHelper log) : base(userHelper)
        {
            _log = log;
        }

        public async Task<ResultClass> AddOrUpdate(SitiosCategorias categoria)
        {
            ResultClass result = new ResultClass();

            string sql = categoria.CategoriaId == 0 ? "INSERT INTO SitiosCategorias(Nombre) VALUES (@Nombre);" : "UPDATE SitiosCategorias SET Nombre = @Nombre WHERE CategoriaId = @CategoriaId;";

            result.data = categoria.CategoriaId == 0 ? await PostData(sql, true, new DynamicParameters(categoria)) : await UpdateData(sql, true, new DynamicParameters(categoria));

            result.state = (int)result.data > 0;

            SitiosCategorias oldVwersion = new();

            if (categoria.CategoriaId > 0)
            {
                oldVwersion = await Find(categoria.CategoriaId);
            }

            await _log.AddLog(new LogDto()
            {
                Descripcion = categoria.CategoriaId == 0 ? "Creación" : $"Actualización",
                Entidad = nameof(SitiosCategorias),
                EntidadId = categoria.CategoriaId == 0 ? (int)result.data : categoria.CategoriaId,
                OldVersionJson = categoria.CategoriaId == 0 ? "" : $"{JsonConvert.SerializeObject(oldVwersion)}",
            });

            return result;
        }

        public async Task<SitiosCategorias> Find(int id)
        {
            return await FilterData<SitiosCategorias>($"SELECT * FROM SitiosCategorias WHERE CategoriaId = {id}");
        }

        public async Task<IEnumerable<LogOutputDto>> GetLogs(int id)
        {
            return await _log.GetLogs(new LogInputDto(id, nameof(SitiosCategorias)));
        }

        public async Task<bool> Remove(int id)
        {
            await _log.AddLog(new LogDto()
            {
                Descripcion = "Eliminación",
                Entidad = nameof(SitiosCategorias),
                EntidadId = id
            });

            return await (RemoveData($"DELETE FROM SitiosCategorias WHERE CategoriaId = {id}")) > 0;
        }

        public async Task<IEnumerable<SitiosCategorias>> Select()
        {
            return await SelectData<SitiosCategorias>("SELECT * FROM SitiosCategorias");
        }
    }
}
