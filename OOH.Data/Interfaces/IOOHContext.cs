using Dapper;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace OOH.Data.Interfaces
{
    interface IOOHContext
    {
        /// <summary>
        /// Devuelve un listado casteado a la clase dada en T
        /// </summary>
        /// <typeparam name="T">Clase a la que se casteara</typeparam>
        /// <param name="_query">Query SQL</param>
        /// <param name="_isProcedure">Definie si será un procedimiento almacenado</param>
        /// <param name="parameters">Parametros</param>
        /// <returns></returns>
        Task<IEnumerable<T>> SelectData<T>(string _query, bool _isProcedure = false, DynamicParameters parameters = null);

        /// <summary>
        /// Devuelve un listado casteado a la clase dada en T apuntando a una base especifica
        /// </summary>
        /// <typeparam name="T">Clase a la que se casteara</typeparam>
        /// <param name="_query">Query SQL</param>
        /// <param name="connection">Cadena de conexión a la base de datos deseada</param>
        /// <param name="_isProcedure">Define si es un procedimiento almacenado</param>
        /// <param name="parameters">Parametros</param>
        /// <returns></returns>
        Task<IEnumerable<T>> SelectData<T>(string _query, string connection, bool _isProcedure = false, DynamicParameters parameters = null);

        /// <summary>
        /// Devuelve un objeto casteado a la clase dada en T
        /// </summary>
        /// <typeparam name="T">Clase a la que se casteara</typeparam>
        /// <param name="_query">Query SQL</param>
        /// <param name="_isProcedure">Define si es un procedimiento almacenado</param>
        /// <param name="parameters">Parametros</param>
        /// <returns></returns>
        Task<T> FilterData<T>(string _query, bool _withParameters = true, DynamicParameters parameters = null, bool _isProcedure = false);

        /// <summary>
        /// Crea un nuevo registro en la base de datos dado los parametros
        /// </summary>
        /// <param name="_query">Query de creacion SQL</param>
        /// <param name="withParameters">Define si se envia o no parametros</param>
        /// <param name="parameters">Parametros (Objeto a guardar en la base)</param>
        /// <param name="_isProcedure">Define si es un procedimiento almacenado</param>
        /// <returns></returns>
        Task<int> PostData(string _query, bool withParameters = true, DynamicParameters parameters = null, bool _isProcedure = false);

        /// <summary>
        /// Remueve un registro en la base de datos
        /// </summary>
        /// <param name="_query">Query de eliminacion SQL</param>
        /// <param name="_withParameters">Define si envia o no parametros</param>
        /// <param name="parameters">Parametros</param>
        /// <param name="_isProcedure">Define si es un procedimiento almacenado</param>
        /// <returns></returns>
        Task<int> RemoveData(string _query, bool _withParameters = false, DynamicParameters parameters = null, bool _isProcedure = false);

        /// <summary>
        /// Actualiza un registro en la base de datos
        /// </summary>
        /// <param name="_query">Query de actualizacion SQL</param>
        /// <param name="_withParameters">Define si envia parametros</param>
        /// <param name="parameters">Parametros (objeto de actualización</param>
        /// <param name="_isProcedure">Define si es un procedimiento almacenado</param>
        /// <returns></returns>
        Task<int> UpdateData(string _query, bool _withParameters = true, DynamicParameters parameters = null, bool _isProcedure = false);
    }
}
