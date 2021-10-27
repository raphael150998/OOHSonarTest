using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GoogleMapGenerator.Dtos
{
    /// <summary>
    /// DTO de entrada de coordenadas para generar el mapa
    /// </summary>
    public class CoordinatesInputDto
    {
        /// <summary>
        /// Latitud
        /// </summary>
        public float Latitude { get; set; }

        /// <summary>
        /// Longitud
        /// </summary>
        public float Longitude { get; set; }
    }
}
