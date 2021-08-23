using Dapper;
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
        private const string StringConnection = "data source=192.168.10.238;initial catalog=OOH_VIVA;user id=jose;password=jr.2021;MultipleActiveResultSets=True;App=EntityFramework";

        public async Task<T> FilterData<T>(string _query, bool _isProcedure = false, DynamicParameters parameters = null, string Connection = StringConnection)
        {
            try
            {
                using (IDbConnection cn = new SqlConnection(StringConnection))
                {
                    var ObjetoReturn = await cn.QuerySingleAsync<T>(_query, param: _isProcedure == true ? parameters : null,
                        commandType: _isProcedure == true ?
                        CommandType.StoredProcedure : CommandType.Text).ConfigureAwait(false);

                    return ObjetoReturn;

                }
            }
            catch (Exception e)
            {

                return (T)Convert.ChangeType(null, typeof(T));

            }

        }

        public async Task<int> PostData(string _query, bool _withParameters = true, DynamicParameters parameters = null, bool _isProcedure = false, string Connection = StringConnection)
        {
            try
            {
                using (IDbConnection cn = new SqlConnection(StringConnection))
                {
                    //En caso de que sea un insert sera necesario devolver el id recien creado
                    _query = _query.ToUpper().Contains("INSERT INTO") ? _query + ";select cast(SCOPE_IDENTITY() as int)" : _query;

                    return (await cn.QueryAsync<int>(_query, param: _withParameters == true ? parameters : null,
                        commandType: _isProcedure == true ?
                        CommandType.StoredProcedure : CommandType.Text).ConfigureAwait(false)).Single();
                }


            }
            catch (Exception e)
            {

                return 0;

            }
        }

        public async Task<IEnumerable<T>> SelectData<T>(string _query, bool _isProcedure = false, DynamicParameters parameters = null, string Connection = StringConnection)
        {
            try
            {
                using (IDbConnection cn = new SqlConnection(StringConnection))
                {
                    var ObjetoReturn = await cn.QueryAsync<T>(_query, param: _isProcedure == true ? parameters : null,
                        commandType: _isProcedure == true ?
                        CommandType.StoredProcedure : CommandType.Text).ConfigureAwait(false);


                    return ObjetoReturn.ToList();
                }
            }
            catch (Exception e)
            {
                return (List<T>)Convert.ChangeType(null, typeof(T));
            }
        }
    }
}
