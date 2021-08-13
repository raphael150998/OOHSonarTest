using Dapper;
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
        public async Task<int> Create(Proveedores proveedor)
        {
            try
            {
                #region sql
                string sql = "INSERT INTO [dbo].[Proveedores] ([Codigo] ,[Nombre] ,[NRC] ,[NIT] ,[Giro] ,[Email] ,[Direccion] ,[Telefono] ,[Celular] ,[PersonaJuridica] ,[Activo] ,[CategoriaId]) VALUES (@Codigo, @Nombre, @NRC, @NIT, @Giro, @Email, @Direccion, @Telefono, @Celular, @PersonaJuridica, @Activo, @CategoriaId);";
                #endregion
                //var parameters = new DynamicParameters({});
                var id = await PostData(sql, true, new DynamicParameters(proveedor));
                return id;
            }
            catch (Exception ex)
            {

                throw;
            }
        }

        public async Task<Proveedores> Find(string _Where = "")
        {
            var ReturnFind = FilterData<Proveedores>("Select * from Proveedores Where = " + _Where).Result;
            return ReturnFind;
        }

        public async Task<int> Remove(string _Where = "")
        {
            return PostData("Update Proveedores set Activo = false where ProveedorId = " + _Where).Result;
        }

        public async Task<IEnumerable<Proveedores>> Select(string _Where = "")
        {
            var ReturnSelect = SelectData<Proveedores>("Select * from Proveedor " + _Where).Result.ToList();
            return ReturnSelect;
        }

        public async Task<int> Update(string _Where = "")
        {
            throw new NotImplementedException();
        }
    }
}
