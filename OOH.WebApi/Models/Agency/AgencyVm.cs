using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace OOH.WebApi.Models.Agency
{
    /// <summary>
    /// DTO web de entrada para agregar/editar una nueva agencia
    /// </summary>
    public class AgencyVm
    {
        public AgencyVm()
        {
            Id = 0;
        }

        /// <summary>
        /// Identificador de la agencia a a editar (por defecto 0 si es nueva)
        /// </summary>
        public int Id { get; set; }

        /// <summary>
        /// Nombre de la agencia
        /// </summary>
        public string Name { get; set; }


        /// <summary>
        /// Comision
        /// </summary>
        public float Rate { get; set; }
    }
}
