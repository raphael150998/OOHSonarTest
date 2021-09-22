using OOH.Data.Dtos;
using OOH.Data.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OOH.Data.Interfaces
{
    public interface ILogHelper
    {
        /// <summary>
        /// Obtiene un log por su id
        /// </summary>
        /// <param name="id">Identificador del log</param>
        /// <returns></returns>
        Task<Log> Find(int id);

        /// <summary>
        /// Obtiene un listado de logs que pueden ser filtrados con el parametro where
        /// </summary>
        /// <param name="where"></param>
        /// <returns></returns>
        Task<IEnumerable<Log>> Select(string where = "");

        /// <summary>
        /// Agrega un nuevo log
        /// </summary>
        /// <param name="log"></param>
        /// <returns></returns>
        Task AddLog(LogDto log);
    }
}
