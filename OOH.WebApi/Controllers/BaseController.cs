using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
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
            new LanguageHelper(_httpContextAccessor).SetLanguage("en");
        }
    }
}
