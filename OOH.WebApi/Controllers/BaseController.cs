using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using Newtonsoft.Json;
using OOH.Data.Dtos.Usuario;
using OOH.Language;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace OOH.WebApi.Controllers
{
    public class BaseController : Controller
    {
        private readonly IHttpContextAccessor _httpContextAccessor;

        public BaseController(IHttpContextAccessor httpContextAccessor)
        {
            _httpContextAccessor = httpContextAccessor;
        }

        public String txtConectionString()
        {
            string txtConnection = User.Claims.Where(x => x.Type == "Cs").FirstOrDefault().Value;
            if (txtConnection != "" && txtConnection != null)
            {
                //UserPermisoDto JsonListPermisos = JsonConvert.DeserializeObject<UserPermisoDto>(jsPermiso);
                return txtConnection;
            }
            else
            {
                return null;
            }
        }
        public UserPermisoDto Permisos()
        {
            string jsPermiso = User.Claims.Where(x => x.Type == "ListPermisos").FirstOrDefault().Value;
            if (jsPermiso != "" && jsPermiso != null)
            {
                UserPermisoDto JsonListPermisos = JsonConvert.DeserializeObject<UserPermisoDto>(jsPermiso);
                return JsonListPermisos;
            }
            else
            {
                return new UserPermisoDto();
            }
        }
        public  override void  OnActionExecuting(ActionExecutingContext context)
        {
            string lang = null;
            string langCookie = Request.Cookies["culture"];
            if (langCookie != null)
            {
                lang = langCookie;
            }
            else
            {
                //object userLanguage = null;
                ////var userLanguage = Request.UserLanguages;
                //var userLang = userLanguage != null ? userLanguage[0] : "";
                //if (userLang != "")
                //{
                //    lang = userLang;
                //}
                //else
                //{
                //    lang = LanguageHelper.GetDefaultLanguage();
                //}
                lang = LanguageHelper.GetDefaultLanguage();
            }
            new LanguageHelper(_httpContextAccessor).SetLanguage(lang);
        }
    }
}
