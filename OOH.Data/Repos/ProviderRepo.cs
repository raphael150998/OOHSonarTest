using Dapper;
using Newtonsoft.Json;
using OOH.Data.Dtos;
using OOH.Data.Dtos.Logs;
using OOH.Data.Dtos.Site;
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
    public class ProviderRepo : AddressRepo
    {
        private readonly ILogHelper _log;

        public ProviderRepo(IWebUserHelper userHelper, ILogHelper log) : base(userHelper)
        {
            _log = log;
        }

        public async Task<ResultClass> AddOrUpdate(Proveedores proveedor)
        {
            ResultClass result = new ResultClass();

            #region sql
            string sql = proveedor.ProveedorId == 0 ?
                            "INSERT INTO [dbo].[Proveedores] ([Codigo],[RazonSocial],[MunicipioId] ,[Nombre] ,[NRC] ,[NIT] ,[Giro] ,[Email] ,[Direccion] ,[Telefono] ,[Celular] ,[PersonaJuridica] ,[Activo] ,[CategoriaId]) VALUES (@Codigo, @RazonSocial,@MunicipioId ,@Nombre, @NRC, @NIT, @Giro, @Email, @Direccion, @Telefono, @Celular, @PersonaJuridica, @Activo, @CategoriaId)"
                            : "UPDATE [dbo].[Proveedores] SET [Codigo] = @Codigo,[RazonSocial] = @RazonSocial, [MunicipioId] = @MunicipioId ,[Nombre] = @Nombre,[NRC] = @NRC ,[NIT] = @NIT,[Giro] = @Giro,[Email] = @Email,[Direccion] = @Direccion,[Telefono] = @Telefono,[Celular] = @Celular,[PersonaJuridica] = @PersonaJuridica,[Activo] = @Activo,[CategoriaId] = @CategoriaId where ProveedorId = @ProveedorId";
            #endregion

            result.data = proveedor.ProveedorId == 0 ? await PostData(sql, true, new(proveedor)) :await UpdateData(sql, true, new(proveedor));

            result.state = (int)result.data > 0;

            Proveedores oldVwersion = new();

            if (proveedor.ProveedorId > 0)
            {
                oldVwersion = await Find(proveedor.ProveedorId);
            }

            await _log.AddLog(new LogDto()
            {
                Descripcion = proveedor.ProveedorId == 0 ? "Creación" : "Actualización",
                Entidad = nameof(Proveedores),
                EntidadId = proveedor.ProveedorId == 0 ? (int)result.data : proveedor.ProveedorId,
                OldVersionJson = proveedor.ProveedorId == 0 ? "" : $"{JsonConvert.SerializeObject(oldVwersion)}",
            });

            return result;
        }

        public async Task<Proveedores> Find(long id)
        {
            Proveedores objeto = await FilterData<Proveedores>($"Select * from Proveedores Where ProveedorId = {id}");
            objeto.DepartamentoId = GetDepartamentoByMunicipioId(objeto.MunicipioId).Result.DepartamentoId;
            return objeto;
        }

        public async Task<IEnumerable<LogOutputDto>> GetLogs(long id)
        {
            return await _log.GetLogs(new LogInputDto(id, nameof(Proveedores)));
        }

        public async Task<bool> Remove(long id)
        {
            await _log.AddLog(new LogDto()
            {
                Descripcion = "Eliminación",
                Entidad = nameof(Proveedores),
                EntidadId = id
            });

            return await (RemoveData($"DELETE FROM Proveedores WHERE ProveedorId = {id}")) > 0;
        }


        public async Task<Select2PagingOutputDto> GetListForSelect2(Select2PagingInputDto request)
        {
            Select2PagingOutputDto modelReturn = new();

            #region sqlCount
            string sqlCount = $"SELECT COUNT(T0.Id) FROM (SELECT ProveedorId [Id], Nombre [Text], ~Activo [Disabled] FROM Proveedores) T0";
            #endregion

            string whereClause = "";

            if (request.Search.Count() > 0)
                whereClause = $" WHERE {string.Join(" OR ", request.Search.Select(x => $"Text like '%{x.ToUpper()}%'"))}";

            sqlCount += whereClause;

            int total = await FilterData<int>(sqlCount);

            int itemsToSkip = request.ItemsPerPage * (request.CurrentPage - 1);

            #region sql
            string sql = $"SELECT T0.* FROM (SELECT ProveedorId [Id], Nombre [Text], ~Activo [Disabled] FROM Proveedores) T0 {whereClause} ORDER BY Id OFFSET({itemsToSkip}) ROWS FETCH NEXT({request.ItemsPerPage}) ROWS ONLY ";

            string sqlSelected = $"SELECT TOP(1) T0.* FROM (SELECT ProveedorId [Id], Nombre [Text], ~Activo [Disabled] FROM Proveedores) T0 WHERE Id = {request.Id}";

            string sqlEdit = $"{sqlSelected} UNION ALL SELECT * FROM ({sql} T1)";
            #endregion

            modelReturn.Results = (await SelectData<Select2OutputDto>(request.Id == 0 ? sql : sqlEdit)).ToList();

            //if (!string.IsNullOrEmpty(request.Id))
            //{
            //    #region sqlSelected              

            //    #endregion

            //    Select2OutputDto selectedOption = await FilterData<Select2OutputDto>(sqlSelected);

            //    selectedOption.Selected = true;

            //    modelReturn.Results.Add(selectedOption);
            //    modelReturn.Results = modelReturn.Results.OrderBy(x => x.Id).ToList();
            //}

            modelReturn.Pagination.More = (request.CurrentPage * request.ItemsPerPage) < total;

            return modelReturn;
        }

        public async Task<IEnumerable<Proveedores>> Select()
        {
            return await SelectData<Proveedores>("Select * from Proveedores");
        }
        
        public async Task<IEnumerable<ProveedoresCategorias>> Category()
        {
            return await SelectData<ProveedoresCategorias>("Select * from ProveedoresCategorias");
        }
    }
}
