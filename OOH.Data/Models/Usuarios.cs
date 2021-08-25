using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OOH.Data.Models
{
    public class Usuarios
    {
        [Required]
        public Int64 UserId { get; set; }

        [Required]
        [Display(Name = "Login")]
        [StringLength(50)]
        public string Login { get; set; }

        [Required]
        [Display(Name = "Contraseña")]
        [StringLength(50)]
        public string Pass { get; set; }
        
        [Required]
        [Display(Name = "Email")]
        [StringLength(50)]
        public string Correo { get; set; }
        
        [Required]
        [Display(Name = "Nombre")]
        [StringLength(100)]
        public string Username { get; set; }
        public int PerfilId { get; set; }
        public int EmpresaId { get; set; }
        public bool Activo { get; set; }

    }
}
