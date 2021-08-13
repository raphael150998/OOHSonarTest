using OOH.Data.Models;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
namespace OOH.Data.Interfaces
{
    public interface IProveedorRepository
    {
        Task<Proveedores> Find(string _Where = "");
        Task<IEnumerable<Proveedores>> Select(string _Where = "");
        Task<int> Update(string _Where = "");
        Task<int> Create(Proveedores proveedor);
        Task<int> Remove(string _Where = "");


    }
}
