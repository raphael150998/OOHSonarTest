using Dapper;
using OOH.Data.Dtos;
using OOH.Data.Helpers;
using OOH.Data.Interfaces;
using OOH.Data.Models;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Text;
using System.Threading.Tasks;

namespace OOH.Data.Repos
{
    public class LogHelper : ILogHelper
    {
        private readonly IWebUserHelper _userHelper;
        private readonly string _connectionString;

        public LogHelper(IWebUserHelper userHelper)
        {
            _userHelper = userHelper;
            _connectionString = _userHelper.GetUserConnectionString() ?? "data source=192.168.10.238;initial catalog=OOH_Seguridad;user id=jose;password=JR.2021;MultipleActiveResultSets=True;App=EntityFramework";
        }

        public async Task<Log> Find(int id)
        {
            using (IDbConnection cn = new SqlConnection(_connectionString))
            {
                return await cn.QueryFirstOrDefaultAsync<Log>($"SELECT * FROM Log WHERE Id = {id}");
            }
        }

        public async Task<IEnumerable<Log>> Select(string where = "")
        {
            using (IDbConnection cn = new SqlConnection(_connectionString))
            {
                return await cn.QueryAsync<Log>($"SELECT * FROM Log {where}");
            }
        }

        public async Task AddLog(LogDto log)
        {
            Log logDb = new()
            {
                Descripcion = log.Descripcion,

                Entidad = log.Entidad,
                EntidadId = log.EntidadId,
                Fecha = DateTimeOffset.Now,
                PlataformaId = _userHelper.GetUserPlatform(),
                UserId = _userHelper.GetUserId(),
                Version = log.Version ?? _userHelper.GetVersion()
            };

            string sql = "INSERT INTO Log (UserId, Entidad, Descripcion, Fecha, EntidadId, PlataformaId, Version) VALUES (@UserId, @Entidad, @Descripcion, @Fecha, @EntidadId, @PlataformaId, @Version);";

            using (IDbConnection cn = new SqlConnection(_connectionString))
            {
                var ObjetoReturn = await cn.ExecuteAsync(sql, new DynamicParameters(logDb));
            }

        }
    }
}
