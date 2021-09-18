using Dapper;
using OOH.Data.Helpers;
using OOH.Data.Interfaces;
using OOH.Data.Models;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace OOH.Data.Repos
{
    public class LogRepository : OOHContext
    {
        public LogRepository(IWebUserHelper userHelper) : base(userHelper)
        {
        }

        public async Task Add(Log log)
        {
            string sql = "INSERT INTO Log (UserId, Entidad, Descripcion, Fecha, EntidadId, PlataformaId, Version) VALUES (@UserId, @Entidad, @Descripcion, @Fecha, @EntidadId, @PlataformaId, @Version);";

            await PostData(sql, true, new DynamicParameters(log));
        }

        public async Task<Log> Find(int id)
        {
            return await FilterData<Log>($"SELECT * FROM Log WHERE Id = {id}");
        }

        public async Task<IEnumerable<Log>> Select(string where = "")
        {
            return await SelectData<Log>("SELECT * FROM Log " + where);
        }
    }
}
