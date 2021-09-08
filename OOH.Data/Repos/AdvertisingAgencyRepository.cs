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
        public AdvertisingAgencyRepository(IWebUserHelper userHelper) : base(userHelper)
        {
        }

        public async Task<int> Create(AgenciasPublicidad agencia)
        {
            string sql = "INSERT INTO AgenciasPublicidad(Nombre, Comision, Activo) VALUES (@Nombre, @Comision, @Activo)";
            var id = await PostData(sql, true, new DynamicParameters(agencia), false);
            return id;
        }

        public async Task<AgenciasPublicidad> Find(int id)
        {
            return await FilterData<AgenciasPublicidad>($"SELECT * FROM AgenciasPublicidad WHERE AgenciaId = {id}", false, null);
        }

        public async Task Remove(int id)
        {
            await PostData($"UPDATE AgenciasPublicidad SET Activo = false WHERE AgenciaId = {id}", false, null, false);
        }

        public async Task<IEnumerable<AgenciasPublicidad>> Select(string _Where = "")
        {
            return SelectData<AgenciasPublicidad>("SELECT * FROM AgenciasPublicidad " + _Where, false, null).Result.ToList();
        }

        public async Task Update(AgenciasPublicidad agencia)
        {
            string sql = "UPDATE AgenciasPublicidad SET Nombre = @Nombre, Comision = @Comision, Activo = @Activo WHERE AgenciaId = @AgenciaId";

            await PostData(sql, true, new DynamicParameters(agencia), false);
        }
    }
}
