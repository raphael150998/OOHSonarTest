using MediatR;
using System;
using System.Collections.Generic;
using System.Text;

namespace OOH.Data.Queries.Proveedor
{
    public class GetProveedoresPaged : IRequest<List<Models.Proveedor>>
    {
        public string Codigo { get; set; }

        public int Categoria { get; set; }

        public string Nombre { get; set; }

        public bool PersonaJuridica { get; set; }

        public bool Activo { get; set; }
    }
}
