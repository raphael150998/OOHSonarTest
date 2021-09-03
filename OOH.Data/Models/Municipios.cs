using System;
using System.Collections.Generic;
using System.Text;

namespace OOH.Data.Models
{
    public class Municipios
    {
        public int MunicipioId { get; set; }
        public int DepartamentoId { get; set; }
        public string Departamento { get; set; }
        public string Nombre { get; set; }
    }
}
