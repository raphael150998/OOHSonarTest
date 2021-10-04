using Dapper;
using OOH.Data.Dtos;
using OOH.Data.Helpers;
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
    public class ProveedorRepository : OOHContext, IBaseRepository<Proveedores>
    {
        private readonly ILogHelper _log;

        public ProveedorRepository(IWebUserHelper userHelper, ILogHelper log) : base(userHelper)
        {
            _log = log;
        }

        public async Task<ResultClass> AddOrUpdate(Proveedores proveedor)
        {
            ResultClass result = new ResultClass();

            #region sql
            string sql = proveedor.ProveedorId == 0 ?
                            "INSERT INTO [dbo].[Proveedores] ([Codigo] ,[Nombre] ,[NRC] ,[NIT] ,[Giro] ,[Email] ,[Direccion] ,[Telefono] ,[Celular] ,[PersonaJuridica] ,[Activo] ,[CategoriaId]) VALUES (@Codigo, @Nombre, @NRC, @NIT, @Giro, @Email, @Direccion, @Telefono, @Celular, @PersonaJuridica, @Activo, @CategoriaId)"
                            : "UPDATE [dbo].[Proveedores] SET [Codigo] = @Codigo ,[Nombre] = @Nombre,[NRC] = @NRC ,[NIT] = @NIT,[Giro] = @Giro,[Email] = @Email,[Direccion] = @Direccion,[Telefono] = @Telefono,[Celular] = @Celular,[PersonaJuridica] = @PersonaJuridica,[Activo] = @Activo,[CategoriaId] = @CategoriaId where ProveedorId = @ProveedorId";
            #endregion

            result.data = proveedor.ProveedorId == 0 ? await PostData(sql, true, new(proveedor)) : UpdateData(sql, true, new(proveedor));

            result.state = (int)result.data > 0;

            await _log.AddLog(new LogDto()
            {
                Descripcion = proveedor.ProveedorId == 0 ? "Creación" : "Actualización",
                Entidad = nameof(Proveedores),
                EntidadId = proveedor.ProveedorId == 0 ? (int)result.data : proveedor.ProveedorId,
            });

            return result;
        }

        public async Task<Proveedores> Find(int id)
        {
            return await FilterData<Proveedores>($"Select * from Proveedores Where ProveedorId = {id}");
        }

        public async Task<IEnumerable<LogOutputDto>> GetLogs(int id)
        {
            return await _log.GetLogs(new LogInputDto(id, nameof(Proveedores)));
        }

        public async Task<IEnumerable<Proveedores>> Select(string _Where = "")
        {
            return (await SelectData<Proveedores>("Select * from Proveedores " + _Where)).ToList();
        }

        public async Task<bool> Remove(int id)
        {
            await _log.AddLog(new LogDto()
            {
                Descripcion = "Eliminación",
                Entidad = nameof(Proveedores),
                EntidadId = id
            });

            return await (RemoveData($"DELETE FROM Proveedores WHERE ProveedorId = {id}")) > 0;
        }
    }
}
