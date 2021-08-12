using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OOH.Data.Models
{
    public class UsuariosModulos
    {
        public int ModuloId { get; set; }

        [StringLength(50)]
        public string Descripcion { get; set; }

    }
}
