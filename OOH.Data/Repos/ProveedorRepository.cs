using OOH.Data.Interfaces;
using OOH.Data.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OOH.Data.Repos
{
    /// <summary>
    /// Todas las variables son en honor a Don Lord Señor Carroña
    /// </summary>
    public class ProveedorRepository : OOHContext, IProveedorRepository
    {
        public async Task<Proveedores> Create(string _Where = "")
        {
            throw new NotImplementedException();
        }

        public async Task<Proveedores> Find(string _Where = "")
        {
            var ReturnFind = FilterData<Proveedores>("Select * from Proveedores Where = " + _Where).Result;
            return ReturnFind;
        }

        public async Task<int> Remove(string _Where = "")
        {
            return PostData("Update Proveedores set Activo = false where ProveedorId = "+ _Where).Result;
        }

        public async Task<IEnumerable<Proveedores>> Select(string _Where = "")
        {
            var ReturnSelect = SelectData<Proveedores>("Select * from Proveedor " + _Where).Result.ToList();
            return ReturnSelect;
        }

        public async Task<Proveedores> Update(string _Where = "")
        {
            throw new NotImplementedException();
        }
    }
}
