using OOH.Data.Models;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
namespace OOH.Data.Interfaces
{
    interface IProveedorRepository 
    {
        Task<Proveedores> Find(string _Where = "");
        Task<IEnumerable<Proveedores>> Select(string _Where = "");
        Task<Proveedores> Update(string _Where = "");
        Task<Proveedores> Create(string _Where = "");
        Task<int> Remove(string _Where = "");
        
        
    }
}
