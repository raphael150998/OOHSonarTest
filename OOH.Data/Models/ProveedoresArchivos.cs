using System;
using System.Collections.Generic;
using System.Text;

namespace OOH.Data.Models
{
    public class ProveedoresArchivos
    {
        public int Id { get; set; }
        public Int64 ProveedorId { get; set; }
        public string Descripcion { get; set; }
        public string Url { get; set; }
    }
}
