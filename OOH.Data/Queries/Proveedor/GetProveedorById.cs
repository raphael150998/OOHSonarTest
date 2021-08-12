using MediatR;
using System;
using System.Collections.Generic;
using System.Text;

namespace OOH.Data.Queries.Proveedor
{
    public class GetProveedorById : IRequest<Models.Proveedor>
    {
        public int Id { get; set; }
    }
}
