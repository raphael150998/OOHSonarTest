using Dapper;
using OOH.Data.Dtos.Usuario;
using OOH.Data.Helpers;
using OOH.Data.Herlpers;
using OOH.Data.Interfaces;
using OOH.Data.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace OOH.Data.Repos
{
    public class AccountRepository : OOHContext
    {
        public AccountRepository(IWebUserHelper userHelper) : base(userHelper)
        {
        }

        public ResultClass ValidarLogin(string Login, string Password)
        {
            try
            {
                if ((Login == "" || Password == "") || (Login == null || Password == null))
                {

                    return new ResultClass() { message = "El campo login y contraseña son requeridos", state = false };

                }

                DynamicParameters parameters = new DynamicParameters();
                parameters.Add("@Login", Login);
                parameters.Add("@Pass", EncryptClass.EncripTar(Password.Trim()));

                var user = FilterData<Usuarios>("SP_Login", true, parameters);
                if (user.Result == null)
                {

                    return new ResultClass() { message = "El usuario no se encuentra registrado o esta pendiente de validacion", state = false };
                }
                Empresa empresa = FilterData<Empresa>("Select * from dbo.Empresa Where EmpresaId = " + user.Result.EmpresaId).Result;

                string LstPermisosQuery = string.Format("SELECT * FROM UsuariosPermisos WHERE (PerfilId = {0}) AND (PlataformaId = 1) OR (PlataformaId = 2)", user.Result.PerfilId);
                var ListaPermisos = SelectData<UsuariosPermisos>(LstPermisosQuery, empresa.ConnectionString);

                UserPermisoDto userPermiso = new UserPermisoDto() { User = user.Result, Permisos = ListaPermisos.Result, StringConecction = empresa.ConnectionString };

                return new ResultClass() { data = userPermiso, state = true };
            }
            catch (Exception ex)
            {
                return new ResultClass() { exception = ex, state = false, message = "Error", data = 0 };
            }
        }


        public ResultClass RegistroUser(Usuarios registro)
        {
            if (registro.Pass.Length < 8)
            {
                return new ResultClass() { data = 2, message = "La contraseña debe tener mas de 8 letras", state = false };
            }

            DynamicParameters parameters = new DynamicParameters();
            parameters.Add("@correo", registro.Correo);
            parameters.Add("@login", registro.Login);
            parameters.Add("@pass", EncryptClass.EncripTar(registro.Pass.Trim()));
            parameters.Add("@perfil", 1);
            //parameters.Add("@perfil", registro.Perfil);
            parameters.Add("@activo", registro.Activo);
            parameters.Add("@empresa", 1);
            parameters.Add("@idioma", 1);
            parameters.Add("@username", registro.Username);
            var success = PostData("SP_Register", true, parameters, true);
            if (success.Result == 0)
            {
                return new ResultClass() { data = 1, state = false, message = "Su usuario no se logro guardar " };

            }
            return new ResultClass();
        }
    }
}
