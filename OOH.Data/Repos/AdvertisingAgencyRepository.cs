using Dapper;
using OOH.Data.Interfaces;
using OOH.Data.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OOH.Data.Repos
{
    public class AdvertisingAgencyRepository : OOHContext, IAdvertisingAgencyRepository
    {
        private readonly IWebUserHelper _userHelper;
        private readonly string _connectionString;

        public AdvertisingAgencyRepository(IWebUserHelper userHelper)
        {
            _userHelper = userHelper;
            _connectionString = _userHelper.GetUserConnectionString();
        }

        public async Task<int> Create(AgenciasPublicidad agencia)
        {
            string sql = "INSERT INTO AgenciasPublicidad(Nombre, Comision, Activo) VALUES (@Nombre, @Comision, @Activo)";
            var id = await PostData(sql, true, new DynamicParameters(agencia), false, _connectionString);
            return id;
        }

        public async Task<AgenciasPublicidad> Find(int id)
        {
            return await FilterData<AgenciasPublicidad>($"SELECT * FROM AgenciasPublicidad WHERE AgenciaId = {id}", false, null, _connectionString);
        }

        public async Task Remove(int id)
        {
            await PostData($"UPDATE AgenciasPublicidad SET Activo = false WHERE AgenciaId = {id}", false, null, false, _connectionString);
        }

        public async Task<IEnumerable<AgenciasPublicidad>> Select(string _Where = "")
        {
            return SelectData<AgenciasPublicidad>("SELECT * FROM AgenciasPublicidad " + _Where, false, null, _connectionString).Result.ToList();
        }

        public async Task Update(AgenciasPublicidad agencia)
        {
            string sql = "UPDATE AgenciasPublicidad SET Nombre = @Nombre, Comision = @Comision, Activo = @Activo WHERE AgenciaId = @AgenciaId";

            await PostData(sql, true, new DynamicParameters(agencia), false, _connectionString);
        }
    }
}
