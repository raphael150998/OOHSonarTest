using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using OOH.Data.Dtos.Usuario;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace OOH.WebApi.ApiControllers
{
    [ApiController]
    public class BaseApiController : ControllerBase
    {
        public string ConnectionString { get; set; }
        public UserPermisoDto PermisoUser { get; set; }

        [Route("api/BlackClover/304")]
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
        [Route("api/TateNoYuusha/27")]
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
    }
}
