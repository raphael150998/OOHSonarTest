using Microsoft.AspNetCore.Http;
using OOH.Data.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OOH.Data.Repos
{
    public class WebUserHelper : IWebUserHelper
    {
        private readonly IHttpContextAccessor _httpContextAccessor;

        public WebUserHelper(IHttpContextAccessor httpContextAccessor)
        {
            _httpContextAccessor = httpContextAccessor;
        }

        public string GetUserConnectionString()
        {
            var user = _httpContextAccessor.HttpContext.User;

            return user.Claims.Where(x => x.Type == "Cs").FirstOrDefault().Value;
        }
    }
}
