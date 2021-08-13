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
        private static string StringConnection = "data source=192.168.10.238;initial catalog=OOH_VIVA;user id=jose;password=jr.2021;MultipleActiveResultSets=True;App=EntityFramework";

        public async Task<T> FilterData<T>(string _query, bool _isProcedure = false, DynamicParameters parameters = null)
        {
            using (IDbConnection cn = new SqlConnection(StringConnection))
            {
                var ObjetoReturn = await cn.QuerySingleAsync<T>(_query, param: _isProcedure == true ? parameters : null,
                    commandType: _isProcedure == true ?
                    CommandType.StoredProcedure : CommandType.Text).ConfigureAwait(false);

                return ObjetoReturn;

            }
        }

        public async Task<int> PostData(string _query, bool withParameters = true, DynamicParameters parameters = null, bool _isProcedure = false)
        {
            using (IDbConnection cn = new SqlConnection(StringConnection))
            {

                var ObjetoReturn = await cn.ExecuteAsync(_query, param: withParameters == true ? parameters : null,
                    commandType: _isProcedure == true ?
                    CommandType.StoredProcedure : CommandType.Text).ConfigureAwait(false);


                return ObjetoReturn;
            }
        }

        public async Task<IEnumerable<T>> SelectData<T>(string _query, bool _isProcedure = false, DynamicParameters parameters = null)
        {
            using (IDbConnection cn = new SqlConnection(StringConnection))
            {
                var ObjetoReturn = await cn.QueryAsync<T>(_query, param: _isProcedure == true ? parameters : null,
                    commandType: _isProcedure == true ?
                    CommandType.StoredProcedure : CommandType.Text).ConfigureAwait(false);


                return ObjetoReturn.ToList();
            }
        }
    }
}
