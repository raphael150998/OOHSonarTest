using OOH.Data.Dtos.Logs;
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
        /// Obtiene un listado de logs por medio de los parametros enviados en request
        /// </summary>
        /// <param name="request">Filtros para los logs que se quieren recuperar</param>
        /// <returns></returns>
        Task<IEnumerable<LogOutputDto>> GetLogs(LogInputDto request);

        /// <summary>
        /// Agrega un nuevo log
        /// </summary>
        /// <param name="log"></param>
        /// <returns></returns>
        Task AddLog(LogDto log);
    }
}
