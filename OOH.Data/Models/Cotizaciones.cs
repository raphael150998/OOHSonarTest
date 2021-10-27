using System;
using System.Collections.Generic;
using System.Text;

namespace OOH.Data.Models
{
    public class Cotizaciones
    {
        public Int64 CotizacionId { get; set; }
        public string Estado { get; set; }
        public int EstadoId { get; set; }
        public Int64 UserId { get; set; }
        public string User { get; set; }
        public string Fecha { get; set; }
        public string Cliente { get; set; }
        public Int64 ClienteId { get; set; }
        public string Agencia { get; set; }
        public int AgenciaId { get; set; }
        public string AtencionA { get; set; }
        public string Comentarios { get; set; }
        public bool ConsolidaCostos { get; set; }
    }
}
