using System;
using System.Collections.Generic;
using System.Text;

namespace OOH.Data.Models
{
    public class ClientesContactos
    {
        public Int64 Id { get; set; }
        public Int64 ClienteId { get; set; }
        public string Nombres { get; set; }
        public string Apellidos { get; set; }
        public int Rol { get; set; }
        public string Email { get; set; }
        public string Telefono{ get; set; }
        public string Celular { get; set; }

    }
}
