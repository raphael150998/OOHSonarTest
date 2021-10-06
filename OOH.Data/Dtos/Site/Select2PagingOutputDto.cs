using OOH.Data.Dtos.Paging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OOH.Data.Dtos.Site
{
    /// <summary>
    /// DTO de salida para lista paginada de datos en formato para la libreria select2.js
    /// </summary>
    public class Select2PagingOutputDto
    {
        public Select2PagingOutputDto()
        {
            Pagination = new()
            {
                More = false
            };
        }

        /// <summary>
        /// listado de datos a paginar
        /// </summary>
        public List<Select2OutputDto> Results { get; set; }

        /// <summary>
        /// Datos de paginacion
        /// </summary>
        public PagingSelect2Dto Pagination { get; set; }
    }
}
