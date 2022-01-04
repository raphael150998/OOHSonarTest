using Dapper;
using OOH.Data.Dtos;
using OOH.Data.Dtos.Logs;
using OOH.Data.Helpers;
using OOH.Data.Interfaces;
using OOH.Data.Models;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OOH.Data.Repos
{
    public class LogHelper : ILogHelper
    {
        private readonly IWebUserHelper _userHelper;
        private readonly string _connectionString;
        private readonly string _userDbConnection;

        public LogHelper(IWebUserHelper userHelper)
        {
            _userHelper = userHelper;
            _userDbConnection = "data source=192.168.10.238;initial catalog=OOH_Seguridad;user id=rafa;password=Orangelemon15;MultipleActiveResultSets=True;App=EntityFramework";
            _connectionString = _userHelper.GetUserConnectionString() ?? "data source=192.168.10.238;initial catalog=OOH_Seguridad;user id=rafa;password=Orangelemon15;MultipleActiveResultSets=True;App=EntityFramework";
        }

        public async Task<Log> Find(int id)
        {
            using IDbConnection cn = new SqlConnection(_connectionString);
            return await cn.QueryFirstOrDefaultAsync<Log>($"SELECT * FROM Log WHERE Id = {id}");
        }

        public async Task<IEnumerable<LogOutputDto>> GetLogs(LogInputDto request)
        {
            List<Usuarios> users = new();

            List<Log> logs = new();

            List<LogOutputDto> modelReturn = new();


            using (IDbConnection cn = new SqlConnection(_connectionString))
            {
                logs = (await cn.QueryAsync<Log>($"SELECT * FROM Log WHERE EntidadId = {request.Id} AND Entidad = '{request.Entidad}'")).ToList();
            }

            List<int> userIds = logs.Select(x => x.UserId).Distinct().ToList();

            if (userIds.Count > 0)
            {
                using (IDbConnection cn = new SqlConnection(_userDbConnection))
                {
                    string sqlUser = $"SELECT UserId, Login, Username FROM Usuarios WHERE UserId in ({string.Join(',', userIds)})";
                    users = (await cn.QueryAsync<Usuarios>(sqlUser)).ToList();
                }

                if (users.Count > 0)
                {
                    foreach (var log in logs)
                    {
                        modelReturn.Add(new()
                        {
                            ActionDate = log.Fecha,
                            Description = log.Descripcion,
                            Login = users.FirstOrDefault(x => x.UserId == log.UserId)?.Login ?? "Sin datos",
                            NameUser = users.FirstOrDefault(x => x.UserId == log.UserId)?.Username ?? "Sin datos",
                            Platform = log.PlataformaId.GetValueString(),
                            Version = log.Version,
                            OldVersionJson = log.OldVersionJson
                        });
                    }
                }
            }

            return modelReturn;
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
                Version = log.Version ?? _userHelper.GetVersion(),
                OldVersionJson = log.OldVersionJson
            };

            string sql = "INSERT INTO Log (UserId, Entidad, Descripcion, Fecha, EntidadId, PlataformaId, Version, OldVersionJson) VALUES (@UserId, @Entidad, @Descripcion, @Fecha, @EntidadId, @PlataformaId, @Version, @OldVersionJson);";

            using IDbConnection cn = new SqlConnection(_connectionString);
            var ObjetoReturn = await cn.ExecuteAsync(sql, new DynamicParameters(logDb));

        }
    }
}
