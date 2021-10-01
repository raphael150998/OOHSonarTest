using OOH.Data.Dtos;
using OOH.Data.Helpers;
using OOH.Data.Models;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace OOH.Data.Interfaces
{
    interface IBaseRepository<T> where T : class
    {
        /// <summary>
        /// Obtiene un registro de tipo <typeparamref name="T"/> de la base de datos dado los parametros <paramref name="Id"/> y <typeparamref name="T"/>
        /// </summary>
        /// <param name="Id">Identificador de la entidad</param>
        /// <returns></returns>
        Task<T> Find(int Id);

        /// <summary>
        /// Obtiene un listado de registros de tipo <typeparamref name="T"/> de la base de datos que puede ser filtrados en ejecucion por medio del parametro <paramref name="_Where"/>
        /// </summary>
        /// <param name="_Where">Filtros</param>
        /// <returns></returns>
        Task<IEnumerable<T>> Select(string _Where = "");

        /// <summary>
        /// Inserta o actualiza un registro de tipo <typeparamref name="T"/> en la base de datos basado en el valor del identificador dado en el parametro <paramref name="collection"/>
        /// </summary>
        /// <param name="collection"></param>
        /// <returns></returns>
        Task<ResultClass> AddOrUpdate(T collection);

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
