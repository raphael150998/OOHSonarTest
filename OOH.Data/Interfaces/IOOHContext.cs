using Dapper;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace OOH.Data.Interfaces
{
    interface IOOHContext
    {
        Task<IEnumerable<T>> SelectData<T>(string _query, bool _isProcedure = false, DynamicParameters parameters = null);
        Task<T> FilterData<T>(string _query, bool _isProcedure = false, DynamicParameters parameters = null);
        Task<int> PostData(string _query, bool withParameters = true, DynamicParameters parameters = null, bool _isProcedure = false);
    }
}
