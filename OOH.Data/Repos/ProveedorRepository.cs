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
    public class ProveedorRepository : OOHContext
    {
        public ProveedorRepository(IWebUserHelper userHelper) : base(userHelper)
        {
        }

        public async Task<int> Create(Proveedores proveedor)
        {
            #region sql
            string sql = "INSERT INTO [dbo].[Proveedores] ([Codigo] ,[Nombre] ,[NRC] ,[NIT] ,[Giro] ,[Email] ,[Direccion] ,[Telefono] ,[Celular] ,[PersonaJuridica] ,[Activo] ,[CategoriaId]) VALUES (@Codigo, @Nombre, @NRC, @NIT, @Giro, @Email, @Direccion, @Telefono, @Celular, @PersonaJuridica, @Activo, @CategoriaId)";
            #endregion
            var id = await PostData(sql, true, new DynamicParameters(proveedor));
            return id;
        }

        public async Task<Proveedores> Find(int id)
        {
            return FilterData<Proveedores>($"Select * from Proveedores Where ProveedorId = {id}").Result;
        }

        public async Task Remove(int id)
        {
            await PostData($"Update Proveedores set Activo = false where ProveedorId = {id}");
        }

        public async Task<IEnumerable<Proveedores>> Select(string _Where = "")
        {
            return SelectData<Proveedores>("Select * from Proveedor " + _Where).Result.ToList();
        }

        public async Task Update(Proveedores proveedor)
        {
            #region sql
            string sql = "UPDATE [dbo].[Proveedores] SET [Codigo] = @Codigo ,[Nombre] = @Nombre,[NRC] = @NRC ,[NIT] = @NIT,[Giro] = @Giro,[Email] = @Email,[Direccion] = @Direccion,[Telefono] = @Telefono,[Celular] = @Celular,[PersonaJuridica] = @PersonaJuridica,[Activo] = @Activo,[CategoriaId] = @CategoriaId where ProveedorId = @ProveedorId";
            #endregion
            await PostData(sql, true, new DynamicParameters(proveedor));
        }
    }
}
