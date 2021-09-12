using Dapper;
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
    public class AdvertisingAgencyRepository : OOHContext, IBaseRepository<AgenciasPublicidad>
    {
        public AdvertisingAgencyRepository(IWebUserHelper userHelper) : base(userHelper)
        {
        }

        public async Task<ResultClass> AddOrUpdate(AgenciasPublicidad agencia)
        {

            ResultClass result = new ResultClass();

            string sql = agencia.AgenciaId == 0 ? "INSERT INTO AgenciasPublicidad(Nombre, Comision, Activo) VALUES (@Nombre, @Comision, @Activo)" : "UPDATE AgenciasPublicidad SET Nombre = @Nombre, Comision = @Comision, Activo = @Activo WHERE AgenciaId = @AgenciaId";

            result.data = agencia.AgenciaId == 0 ? await PostData(sql, true, new DynamicParameters(agencia)) : await UpdateData(sql, true, new DynamicParameters(agencia));

            result.state = (int)result.data > 0;

            return result;
        }

        public async Task<AgenciasPublicidad> Find(int id)
        {
            return await FilterData<AgenciasPublicidad>($"SELECT * FROM AgenciasPublicidad WHERE AgenciaId = {id}");
        }

        public async Task<bool> Remove(int id)
        {
            return RemoveData($"DELETE FROM AgenciasPublicidad WHERE AgenciaId = {id}").Result > 0;
        }

        public async Task<IEnumerable<AgenciasPublicidad>> Select(string _Where = "")
        {
            return await SelectData<AgenciasPublicidad>("SELECT * FROM AgenciasPublicidad " + _Where);
        }
    }
}
