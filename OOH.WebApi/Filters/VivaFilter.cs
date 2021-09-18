using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using OOH.Language;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using OOH.Data;
using System.Security.Claims;
using Newtonsoft.Json;
using OOH.Data.Dtos.Usuario;
using OOH.Data.Models;
using OOH.Data.Helpers;

namespace OOH.WebApi.Filters
{
    public class VivaFilter : IAuthorizationFilter
    {
        private readonly ActionPermission _action;
        private readonly string _permission;
        public VivaFilter(string permission, ActionPermission action)
        {
            _action = action;
            _permission = permission;
        }

        public void OnAuthorization(AuthorizationFilterContext context)
        {
            #region Authentication
            if (!context.HttpContext.User.Identity.IsAuthenticated) { context.Result = new RedirectToActionResult("Login", "Account", null); return; }
            #endregion

            ClaimsPrincipal user = context.HttpContext.User;

            #region Authorization
            if (_action != ActionPermission.NoAction && !string.IsNullOrEmpty(_permission))
            {
                string permissionJson = user.Claims.Where(x => x.Type == "ListPermisos").FirstOrDefault()?.Value ?? "";

                List<UsuariosPermisos> permissions = JsonConvert.DeserializeObject<List<UsuariosPermisos>>(permissionJson);

                UsuariosPermisos permissionRequested = permissions?.FirstOrDefault(x => x.Permiso == _permission);

                if (permissionRequested == null) { context.Result = new StatusCodeResult(StatusCodes.Status403Forbidden); return; }

                bool isActionAllowed = false;

                switch (_action)
                {
                    case ActionPermission.Read:
                        isActionAllowed = permissionRequested.Ver;
                        break;
                    case ActionPermission.Create:
                        isActionAllowed = permissionRequested.Agregar;
                        break;
                    case ActionPermission.Delete:
                        isActionAllowed = permissionRequested.Eliminar;
                        break;
                    case ActionPermission.Update:
                        isActionAllowed = permissionRequested.Modificar;
                        break;
                    case ActionPermission.Execute:
                        isActionAllowed = permissionRequested.Ejecutar;
                        break;
                }

                if (!isActionAllowed) { context.Result = new StatusCodeResult(StatusCodes.Status403Forbidden); return; }

            }
            #endregion

            #region Language
            string lang = null;

            string userLang = !string.IsNullOrEmpty(user.Claims.Where(x => x.Type == "Language").FirstOrDefault()?.Value) ? (EnumHelper.Parse<Languages>(user.Claims.Where(x => x.Type == "Language").FirstOrDefault().Value)).GetValueString() : Languages.es.GetValueString();

            if (userLang != null)
            {
                lang = userLang;
            }
            else
            {
                string langBrowser = (context.HttpContext.Request.GetTypedHeaders()
                       .AcceptLanguage
                       ?.OrderByDescending(x => x.Quality ?? 1) // Quality defines priority from 0 to 1, where 1 is the highest.
                       .Select(x => x.Value.ToString().Split('-').FirstOrDefault())
                       .ToArray() ?? Array.Empty<string>())[0];
                lang = langBrowser;
            }
            new LanguageHelper().SetLanguage(lang);
            #endregion

            #region Log
            if(_action == ActionPermission.NoAction && string.IsNullOrEmpty(_permission))
            {

            }
            #endregion
        }
    }
}