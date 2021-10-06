using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OOH.Data.Dtos.Site
{
    /// <summary>
    /// DTO de salida para listado que utilizan la libreria Select2.js
    /// </summary>
    public class Select2OutputDto
    {
        /// <summary>
        /// Identificador
        /// </summary>
        public string Id { get; set; }

        /// <summary>
        /// Texto a mostrar en la vista
        /// </summary>
        public string Text { get; set; }

        /// <summary>
        /// Determina si el registro esta o no activo
        /// </summary>
        public bool Disabled { get; set; }

        /// <summary>
        /// Determina si el objeto esta seleccionado por defecto
        /// </summary>
        public bool Selected { get; set; }
    }
}
