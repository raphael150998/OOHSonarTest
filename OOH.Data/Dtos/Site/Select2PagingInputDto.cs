using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OOH.Data.Dtos.Site
{
    /// <summary>
    /// DTO de entrada para select2.js paginado
    /// </summary>
    public class Select2PagingInputDto
    {
        public Select2PagingInputDto()
        {
            CurrentPage = 1;
            ItemsPerPage = 10;
            Search = new();
        }

        /// <summary>
        /// Identificador que se buscara para incluir en el listado y que estara seleccionado por defecto
        /// </summary>
        public long Id { get; set; }

        /// <summary>
        /// Pagina en que se encuentra
        /// </summary>
        public int CurrentPage { get; set; }

        /// <summary>
        /// Cantidad de elementos a mostrar en cada pagina
        /// </summary>
        public int ItemsPerPage { get; set; }

        /// <summary>
        ///Claves que se buscan
        /// </summary>
        public List<string> Search { get; set; }
    }
}
