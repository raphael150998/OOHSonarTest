using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PowerPointProvider.Dtos
{
    /// <summary>
    /// DTO de entrada para generar un powerpoint
    /// </summary>
    public class PresentationInputDto
    {
        /// <summary>
        /// Nombre del cliente 
        /// </summary>
        public string Client { get; set; }

        /// <summary>
        /// Listado de caras
        /// </summary>
        public List<SlideFaceInputDto> Faces { get; set; }

        /// <summary>
        /// Determina si la presentacion debe incluir la diapositiva de circuito y a su vez generarlo
        /// </summary>
        public bool HasCircuit { get; set; }

    }
}
