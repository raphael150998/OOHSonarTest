using OOH.Data.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace OOH.Data.Dtos.Usuario
{
    public class UserPermisoDto
    {
        public Usuarios User { get; set; }
        public string StringConecction { get; set; }
        public IEnumerable<UsuariosPermisos> Permisos { get; set; }
    }
}
