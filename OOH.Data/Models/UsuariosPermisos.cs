using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace OOH.Data.Models
{
    public class UsuariosPermisos
    {
        public int Id { get; set; }
        public int PerfilId { get; set; } 
        public int ModuloId { get; set; }
        public Platform PlataformaId { get; set; }
    
        [Required]
        [StringLength(50)]
        public string Permiso { get; set; }
        public bool Ejecutar { get; set; }
        public bool Ver { get; set; }
        public bool Agregar { get; set; }
        public bool Modificar { get; set; }
        public bool Eliminar { get; set; }

    }
}
