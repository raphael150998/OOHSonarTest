using OOH.Data.Dtos.Logs;
using OOH.Data.Helpers;
using OOH.Data.Models;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace OOH.Data.Interfaces
{
    interface IBaseRepo<T> where T : class
    {
        /// <summary>
        /// Obtiene un registro de tipo <typeparamref name="T"/> de la base de datos dado los parametros <paramref name="id"/> y <typeparamref name="T"/>
        /// </summary>
        /// <param name="id">Identificador de la entidad</param>
        /// <returns></returns>
        Task<T> Find(int id);

        /// <summary>
        /// Obtiene un listado de registros de tipo <typeparamref name="T"/> de la base de datos que puede ser filtrados en ejecucion por medio del parametro <paramref name="where"/>
        /// </summary>
        /// <returns></returns>
        Task<IEnumerable<T>> Select();

        /// <summary>
        /// Inserta o actualiza un registro de tipo <typeparamref name="T"/> en la base de datos basado en el valor del identificador dado en el parametro <paramref name="model"/>
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        Task<ResultClass> AddOrUpdate(T model);

        /// <summary>
        /// Remueve un registro de tipo <typeparamref name="T"/> de la base de datos basado en el parametro <paramref name="id"/>
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        Task<bool> Remove(int id);

        /// <summary>
        /// Obtiene los registros de actividad de un registro en la base dado su id y la entidad de interaccion
        /// </summary>
        /// <param name="id">Id de la entidad que se desea obtener los registros de actividad</param>
        /// <returns></returns>
        Task<IEnumerable<LogOutputDto>> GetLogs(int id);

    }
}
