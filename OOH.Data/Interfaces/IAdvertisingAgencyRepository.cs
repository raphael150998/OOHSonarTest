using OOH.Data.Models;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace OOH.Data.Interfaces
{
    public interface IAdvertisingAgencyRepository
    {
        /// <summary>
        ///  Obtiene una agencia de publicidad por su id
        /// </summary>
        /// <param name="id">Id de la agencia de publicidad</param>
        /// <returns cref="AgenciasPublicidad"></returns>
        Task<AgenciasPublicidad> Find(int id);

        /// <summary>
        /// Obtiene un listado de agencias de publicidad en base a los filtros enviados en <paramref name="_Where"/>
        /// </summary>
        /// <param name="_Where">Filtros</param>
        /// <returns><see cref="AgenciasPublicidad"/></returns>
        Task<IEnumerable<AgenciasPublicidad>> Select(string _Where = "");

        /// <summary>
        /// Actualiza una agencia de publicidad en base a su <see cref="AgenciasPublicidad.AgenciaId"/>
        /// </summary>
        /// <param name="agencia de publicidad">Objeto de agencia de publicidad con los datos actualizados</param>
        /// <returns></returns>
        Task Update(AgenciasPublicidad agencia);

        /// <summary>
        /// Crea una nueva agencia de publicidad
        /// </summary>
        /// <param name="agencia de publicidad">Objeto de agencia de publicidad con los datos con que se creara</param>
        /// <returns><see cref="int"/></returns>
        /// <completionlist cref=""/>
        Task<int> Create(AgenciasPublicidad agencia);

        /// <summary>
        /// Remueve una agencia de publicidad de el listado de los activos en base a su <see cref="AgenciasPublicidad.AgenciaId"/>
        /// </summary>
        /// <param name="id">Identificador de la agencia de publicidad a remover</param>
        /// <returns></returns>
        Task Remove(int id);
    }
}
