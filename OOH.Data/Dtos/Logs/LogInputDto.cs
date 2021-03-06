using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OOH.Data.Dtos.Logs
{
    /// <summary>
    /// DTO de entrada para logs
    /// </summary>
    public record LogInputDto
    {
        public LogInputDto(long id, string entidad)
        {
            Id = id;
            Entidad = entidad;
            Valid();
        }


        /// <summary>
        /// IdEntidad del registro que se busca
        /// </summary>
        public long Id { get; set; }

        /// <summary>
        /// Nombre de la entidad
        /// </summary>
        public string Entidad { get; set; }

        public void Valid()
        {
            if (string.IsNullOrEmpty(Entidad))
                throw new ArgumentNullException(nameof(Entidad), "El parametro Entidad no puede ser nulo o estar vacio");
        }
    }
}
