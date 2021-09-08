using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace OOH.WebApi.Controllers
{
    public class BaseController : Controller
    {
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

    }
}
