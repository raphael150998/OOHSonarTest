using Dapper;
using OOH.Data.Dtos.Usuario;
using OOH.Data.Helpers;
using OOH.Data.Herlpers;
using OOH.Data.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace OOH.Data.Repos
{
    public class AccountRepository
    {
        private static OOHContext db = new OOHContext();
        public ResultClass ValidarLogin(string Login, string Password) {
            try
            {
                if ((Login == "" || Password == "") || (Login == null || Password == null))
                {

                    return new ResultClass() { message = "El campo login y contraseña son requeridos", state = false };

                }

                DynamicParameters parameters = new DynamicParameters();
                parameters.Add("@Login", Login);
                parameters.Add("@Pass", EncryptClass.EncripTar(Password.Trim()));

                var user = db.FilterData<Usuarios>("SP_Login", true, parameters);
                if (user.Result == null)
                {

                    return new ResultClass() { message = "El usuario no se encuentra registrado o esta pendiente de validacion", state = false };
                }


                string LstPermisosQuery = string.Format("SELECT * FROM UsuariosPermisos WHERE (PerfilId = {0}) AND (PlataformaId = 1) OR (PlataformaId = 2)", user.Result.Perfil);
                var ListaPermisos = db.SelectData<UsuariosPermisos>(LstPermisosQuery);

                UserPermisoDto userPermiso = new UserPermisoDto() { User = user.Result, Permisos = ListaPermisos.Result };

                return new ResultClass() { data = userPermiso, state = true };
            }
            catch (Exception ex)
            {
                return new ResultClass() { exception = ex, state = false, message = "Error", data = 0 };
            }
        }

    }
}
