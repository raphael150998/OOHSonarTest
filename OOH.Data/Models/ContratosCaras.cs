using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OOH.Data.Models
{
    /// <summary>
    /// Modelo de la tabla relacion ContratosCaras
    /// </summary>
    public class ContratosCaras
    {
        /// <summary>
        /// Identificador
        /// </summary>
        public long Id { get; set; }

        /// <summary>
        /// Id del contrato
        /// </summary>
        public long ContratoId { get; set; }

        /// <summary>
        /// Id de la cara
        /// </summary>
        public long CaraId { get; set; }

        /// <summary>
        /// Costo mensual de arrendamiento
        /// </summary>
        public decimal CostoMensualArrendamiento { get; set; }

        /// <summary>
        /// Costo de impresion
        /// </summary>
        public decimal? CostoImpresion { get; set; }

        /// <summary>
        /// Costo de instalacion
        /// </summary>
        public decimal? CostoInstalacion { get; set; }

        /// <summary>
        /// Costo de saliente
        /// </summary>
        public decimal? CostoSaliente { get; set; }

        /// <summary>
        /// Fecha de inicio de contrato para esta cara
        /// </summary>
        public DateTime Desde { get; set; }

        /// <summary>
        /// Fecha de finalizacion de contrato para esta cara
        /// </summary>
        public DateTime Hasta { get; set; }

        /// <summary>
        /// StandBy
        /// </summary>
        public bool StandBy { get; set; }

        /// <summary>
        /// Base
        /// </summary>
        public decimal Base { get; set; }

        /// <summary>
        /// Altura
        /// </summary>
        public decimal Altura { get; set; }

        /// <summary>
        /// Referencia
        /// </summary>
        public string Referencia { get; set; }

        /// <summary>
        /// Consolida por sitio
        /// </summary>
        public bool ConsolidaPorSitio { get; set; }

        /// <summary>
        /// Determina si esta activo
        /// </summary>
        public bool Activo { get; set; }

    }
}
