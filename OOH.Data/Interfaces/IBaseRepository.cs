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
        Task<T> Find(int Id);
        Task<IEnumerable<T>> Select(string _Where = "");
        Task<ResultClass> AddOrUpdate(T collection);
        Task<bool> Remove(int id);
        //Task<ResultClass> Remove(int id, string condition = "");
        /// <summary>
        /// Obtiene los registros de actividad de un registro en la base dado su id y la entidad de interaccion
        /// </summary>
        /// <param name="id">Id de la entidad que se desea obtener los registros de actividad</param>
        /// <returns></returns>
        Task<IEnumerable<LogOutputDto>> GetLogs(int id);

    }
}
