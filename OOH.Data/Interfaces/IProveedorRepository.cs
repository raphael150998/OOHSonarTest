using OOH.Data.Models;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
namespace OOH.Data.Interfaces
{
    public interface IProveedorRepository
    {
        /// <summary>
        ///  Obtiene un proveedor por su id
        /// </summary>
        /// <param name="Id">Id del proveedor</param>
        /// <returns cref="Proveedores"></returns>
        Task<Proveedores> Find(int Id);

        /// <summary>
        /// Obtiene un listado de proveedores en base a los filtros enviados en <paramref name="_Where"/>
        /// </summary>
        /// <param name="_Where">Filtros</param>
        /// <returns><see cref="Proveedores"/></returns>
        Task<IEnumerable<Proveedores>> Select(string _Where = "");
        
        /// <summary>
        /// Actualiza un proveedor en base a su <see cref="Proveedores.ProveedorId"/>
        /// </summary>
        /// <param name="proveedor">Objeto de proveedor con los datos actualizados</param>
        /// <returns></returns>
        Task Update(Proveedores proveedor);
        
        /// <summary>
        /// Crea un nuevo proveedor
        /// </summary>
        /// <param name="proveedor">Objeto de proveedor con los datos con que se creara</param>
        /// <returns><see cref="int"/></returns>
        /// <completionlist cref=""/>
        Task<int> Create(Proveedores proveedor);
        
        /// <summary>
        /// Remueve un proveedor de el listado de los activos en base a su <see cref="Proveedores.ProveedorId"/>
        /// </summary>
        /// <param name="id">Identificador del proveedor a remover</param>
        /// <returns></returns>
        Task Remove(int id);


    }
}
