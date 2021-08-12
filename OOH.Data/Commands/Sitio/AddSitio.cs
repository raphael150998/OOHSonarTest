using MediatR;
using System;
using System.Collections.Generic;
using System.Text;
using static OOH.Data.Enum;

namespace OOH.Data.Commands.Sitio
{
    public class AddSitio : IRequest
    {
        public string Codigo { get; set; }

        public int ProveedorId { get; set; }

        public string Direccion { get; set; }

        public string Referencia { get; set; }

        public string Sentido { get; set; }

        public float Longitud { get; set; }

        public float Latitud { get; set; }

        public int MunicipioId { get; set; }

        public int ZonaId { get; set; }

        public bool RequierePermiso { get; set; }

        public bool Activo { get; set; }

        public string RegistroCatastral { get; set; }

        public float Altura { get; set; }

        public string Observaciones { get; set; }

        public string EnlaceWeb { get; set; }

    }
}
