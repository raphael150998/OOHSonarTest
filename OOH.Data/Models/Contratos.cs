using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OOH.Data.Models
{
    /// <summary>
    /// Modelo de la tabla contratos
    /// </summary>
    public class Contratos
    {
        public Contratos()
        {
            FechaCreacion = DateTime.Now;
        }
        /// <summary>
        /// Identificador
        /// </summary>
        public long ContratoId { get; set; }

        /// <summary>
        /// Codigo del contrato
        /// </summary>
        public string Codigo { get; set; }

        /// <summary>
        /// Quien creo el contrato
        /// </summary>
        public long EjecutivoId { get; set; }

        /// <summary>
        /// Agencia ligada al contrato 
        /// </summary>
        public int? AgenciaId { get; set; }

        /// <summary>
        /// Cliente del contrato
        /// </summary>
        public long ClienteId { get; set; }

        /// <summary>
        /// Consolidar costos
        /// </summary>
        public bool ConsolidarCostos { get; set; }

        /// <summary>
        /// Determina si requiere instalacion
        /// </summary>
        public bool RequiereInstalacion { get; set; }

        /// <summary>
        /// Indica con quien se tiene contacto directo para el contrato
        /// </summary>
        public string AtencionA { get; set; }

        /// <summary>
        /// Rubro
        /// </summary>
        public int? RubroId { get; set; }

        /// <summary>
        /// Marca
        /// </summary>
        public string Marca { get; set; }

        /// <summary>
        /// Dias bonificados
        /// </summary>
        public int DiasBonificados { get; set; }

        /// <summary>
        /// Promocion
        /// </summary>
        public long? PromocionId { get; set; }

        /// <summary>
        /// Promociones
        /// </summary>
        public string Observaciones { get; set; }

        /// <summary>
        /// Fecha de creacion del contrato
        /// </summary>
        public DateTime FechaCreacion { get; set; }

        /// <summary>
        /// Estado del contrato
        /// </summary>
        public EstadoContrato Estado { get; set; }

        /// <summary>
        /// Determina si esta activo
        /// </summary>
        public bool Activo { get; set; }

    }
}
