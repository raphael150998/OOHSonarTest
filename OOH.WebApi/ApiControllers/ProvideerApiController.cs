using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using OOH.Data.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace OOH.WebApi.ApiControllers
{
    [ApiController]
    public class ProvideerApiController : ControllerBase
    {
        [Route("api/Provideer/Get")]
        public List<Proveedores> GetProveedor()
        {
            List<Proveedores> lst = new List<Proveedores>();
            Proveedores cH = new Proveedores() { ProveedorId = 1, Nombre = "Juan", NRC = "00112233", NIT = "0306-10240000-107-4" };
            lst.Add(cH);
            cH = new Proveedores() { ProveedorId = 2, Nombre = "Jose", NRC = "32543412", NIT = "0306-28042000-101-2"};
            lst.Add(cH);
            cH = new Proveedores() { ProveedorId = 3, Nombre = "Mario", NRC = "09897865", NIT = "0307-03122001-102-2"};
            lst.Add(cH);
            cH = new Proveedores() { ProveedorId  = 4, Nombre = "Lucio", NRC = "12322114", NIT = "0306-11022002-103-5" };
            lst.Add(cH);
            cH = new Proveedores() { ProveedorId = 5, Nombre = "Alberto", NRC = "87670923", NIT = "0306-05081998-107-9" };
            lst.Add(cH);
            return lst;
        }

    }
}
