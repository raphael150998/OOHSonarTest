using GoogleMapGenerator.Dtos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PowerPointProvider.Dtos
{
    /// <summary>
    /// DTO de entrada para una slide
    /// </summary>
    public class SlideFaceInputDto
    {
        /// <summary>
        /// Codigo
        /// </summary>
        public string Code { get; set; }

        /// <summary>
        /// Direccion de referencia
        /// </summary>
        public string Address { get; set; }

        /// <summary>
        /// Tipo de estructura
        /// </summary>
        public string StructureType { get; set; }

        /// <summary>
        /// Ancho de cara
        /// </summary>
        public double Width { get; set; }

        /// <summary>
        /// Alto de cara
        /// </summary>
        public double Height { get; set; }

        /// <summary>
        /// Determina si la cara esta disponible o no 
        /// </summary>
        public bool Available { get; set; }

        /// <summary>
        /// Precio de contratacion
        /// </summary>
        public decimal HiringPrice { get; set; }

        /// <summary>
        /// Precio de imprenta
        /// </summary>
        public decimal PrintPrice { get; set; }

        /// <summary>
        /// Trafico vehicular diario
        /// </summary>
        public int DailyTraffic { get; set; }

        /// <summary>
        /// Sentido
        /// </summary>
        public string Direction { get; set; }

        /// <summary>
        /// Notas
        /// </summary>
        public string Notes { get; set; }

        /// <summary>
        /// Imagen de referencia de la cara
        /// </summary>
        public FileInputDto ReferenceImage { get; set; }

        /// <summary>
        /// Imagen del map con la ubicacion de la cara
        /// </summary>
        public CoordinatesInputDto Map { get; set; }
    }
}
