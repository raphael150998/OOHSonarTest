using System;
using System.Collections.Generic;
using System.Text;

namespace OOH.Data.Models
{
    public class Clientes
    {
        public Int64 ClienteId { get; set; }
        public string Codigo { get; set; }
        public string RazonSocial { get; set; }
        public string NombreComercial { get; set; }
        public string NRC { get; set; }
        public string NIT { get; set; }
        public string Giro { get; set; }
        public string Email { get; set; }
        public string Direccion { get; set; }
        public string Telefono { get; set; }
        public string Celular { get; set; }
        public bool PersonaJuridica { get; set; }
        public int  EjecutivoId { get; set; }
        public int UsuarioId { get; set; }
        public bool Activo { get; set; }
        public int  CategoriaId { get; set; }
        public int  Categoria { get; set; }
        public int  MunicipioId { get; set; }
        public int  Municipio { get; set; }
        public int DepartamentoId { get; set; }

    }
}
