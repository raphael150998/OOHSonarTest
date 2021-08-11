using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace OOH.Data.Models
{
    public class UsuariosPerfiles
    {
        public int PerfilId { get; set; }

        [StringLength(50)]
        public string Descripcion { get; set; }

    }
}
