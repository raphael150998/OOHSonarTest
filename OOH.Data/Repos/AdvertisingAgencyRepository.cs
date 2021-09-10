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
            string sql = "";
            int resultData = 0;
            ResultClass result = new ResultClass();
            if (agencia.AgenciaId == 0)
            {
                sql = "INSERT INTO AgenciasPublicidad(Nombre, Comision, Activo) VALUES (@Nombre, @Comision, @Activo)";
            }
            else
            {
                sql = "UPDATE AgenciasPublicidad SET Nombre = @Nombre, Comision = @Comision, Activo = @Activo WHERE AgenciaId = @AgenciaId";
            }

            resultData = await PostData(sql, true, new DynamicParameters(agencia));

            result.data = resultData;
            result.state = resultData > 0;

            return result;
        }

        public async Task<AgenciasPublicidad> Find(int id)
        {
            return await FilterData<AgenciasPublicidad>($"SELECT * FROM AgenciasPublicidad WHERE AgenciaId = {id}");
        }

        public async Task<bool> Remove(int id)
        {
            throw new NotImplementedException();
        }

        public async Task<IEnumerable<AgenciasPublicidad>> Select(string _Where = "")
        {
            return await SelectData<AgenciasPublicidad>("SELECT * FROM AgenciasPublicidad " + _Where);
        }
    }
}
