using Dapper;
using OOH.Data.Dtos;
using OOH.Data.Interfaces;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OOH.Data
{
    public class OOHContext : IOOHContext
    {
        public readonly IWebUserHelper _userHelper;
        private readonly string _connectionString;
        public OOHContext(IWebUserHelper userHelper)
        {
            _userHelper = userHelper;
            _connectionString = _userHelper.GetUserConnectionString() ?? "data source=192.168.10.238;initial catalog=OOH_Seguridad;user id=jose;password=Cesar1983****;MultipleActiveResultSets=True;App=EntityFramework";
        }

        public async Task<T> FilterData<T>(string _query, bool _isProcedure = false, DynamicParameters parameters = null)
        {
            using (IDbConnection cn = new SqlConnection(_connectionString))
            {
                var ObjetoReturn = await cn.QueryFirstOrDefaultAsync<T>(_query, param: _isProcedure == true ? parameters : null,
                    commandType: _isProcedure == true ?
                    CommandType.StoredProcedure : CommandType.Text).ConfigureAwait(false);

                return ObjetoReturn;

            }
        }

        public async Task<int> RemoveData(string _query, bool _withParameters = false, DynamicParameters parameters = null, bool _isProcedure = false)
        {
            using (IDbConnection cn = new SqlConnection(_connectionString))
            {
                var ObjetoReturn = await cn.ExecuteAsync(_query, param: _withParameters == true ? parameters : null,
                                    commandType: _isProcedure == true ?
                                    CommandType.StoredProcedure : CommandType.Text).ConfigureAwait(false);


                return ObjetoReturn;
            }
        }
        public async Task<int> PostData(string _query, bool _withParameters = true, DynamicParameters parameters = null, bool _isProcedure = false)
        {
            using (IDbConnection cn = new SqlConnection(_connectionString))
            {
                //En caso de que sea un insert sera necesario devolver el id recien creado
                _query = _query.ToUpper().Contains("INSERT INTO") ? _query + ";select cast(SCOPE_IDENTITY() as int)" : _query;
                var ObjetoReturn = await cn.QuerySingleAsync<int>(_query, param: _withParameters == true ? parameters : null,
                                    commandType: _isProcedure == true ?
                                    CommandType.StoredProcedure : CommandType.Text).ConfigureAwait(false);


                return ObjetoReturn;
            }
        }

        public async Task<IEnumerable<T>> SelectData<T>(string _query, bool _isProcedure = false, DynamicParameters parameters = null)
        {
            using (IDbConnection cn = new SqlConnection(_connectionString))
            {
                var ObjetoReturn = await cn.QueryAsync<T>(_query, param: _isProcedure == true ? parameters : null,
                    commandType: _isProcedure == true ?
                    CommandType.StoredProcedure : CommandType.Text).ConfigureAwait(false);


                return ObjetoReturn.ToList();
            }
        }

        public async Task<IEnumerable<T>> SelectData<T>(string _query, string connection, bool _isProcedure = false, DynamicParameters parameters = null)
        {
            using (IDbConnection cn = new SqlConnection(string.IsNullOrEmpty(connection) ? _connectionString : connection))
            {
                var ObjetoReturn = await cn.QueryAsync<T>(_query, param: _isProcedure == true ? parameters : null,
                    commandType: _isProcedure == true ?
                    CommandType.StoredProcedure : CommandType.Text).ConfigureAwait(false);


                return ObjetoReturn.ToList();
            }
        }

        public async Task<int> UpdateData(string _query, bool _withParameters = true, DynamicParameters parameters = null, bool _isProcedure = false)
        {
            using (IDbConnection cn = new SqlConnection(_connectionString))
            {
                int ObjetoReturn = await cn.ExecuteAsync(_query, param: _withParameters == true ? parameters : null,
                                    commandType: _isProcedure == true ?
                                    CommandType.StoredProcedure : CommandType.Text).ConfigureAwait(false);

                return ObjetoReturn;
            }
        }

        public async Task<int> TransactionData<T,T2>(string _queryMaestro, string _queryDetalle, T Maestro , List<T2> Detalle)
        {
            using (var cn = new SqlConnection(_connectionString))
            {
                cn.Open();

                using (var transaction = cn.BeginTransaction())
                {
                    try
                    {
                        DynamicParameters parametersMaestro = new DynamicParameters(Maestro);
                        _queryMaestro = _queryMaestro.ToUpper().Contains("INSERT INTO") ? _queryMaestro + ";select cast(SCOPE_IDENTITY() as int)" : _queryMaestro;
                        var id = await  cn.QuerySingleAsync<int>(_queryMaestro, parametersMaestro, transaction: transaction, commandType: CommandType.Text).ConfigureAwait(false);
                        foreach (var item in Detalle)
                        {
                            DynamicParameters parametersDetalle = new DynamicParameters(item);
                            parametersDetalle.Add("@idMaestro",id);
                           int s= await cn.ExecuteAsync(_queryDetalle,parametersDetalle, transaction: transaction,commandType:CommandType.Text).ConfigureAwait(false);
                        }
                        transaction.Commit();
                        return id;
                    }
                    catch (Exception ex)
                    {
                        transaction.Rollback();
                        return 0;

                    }

                }
            }
        }
    }
}
