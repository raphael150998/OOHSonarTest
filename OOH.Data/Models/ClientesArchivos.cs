using System;
using System.Collections.Generic;
using System.Text;

namespace OOH.Data.Models
{
    public class ClientesArchivos
    {
        public int Id { get; set; }
        public Int64 ClienteId { get; set; }
        public string Descripcion { get; set; }
        public string Url { get; set; }
    }
}
