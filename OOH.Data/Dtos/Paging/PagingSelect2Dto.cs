using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OOH.Data.Dtos.Paging
{
    /// <summary>
    /// paginacion de libreria select2.js
    /// </summary>
    public class PagingSelect2Dto
    {
        /// <summary>
        /// Determina si existen mas datos por paginar
        /// </summary>
        public bool More { get; set; }
    }
}
