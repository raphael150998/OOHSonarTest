using Microsoft.AspNetCore.Http;
using OOH.Data.Helpers;
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
        private string connectionString { get; set; } = "";

        public WebUserHelper(IHttpContextAccessor httpContextAccessor)
        {
            _httpContextAccessor = httpContextAccessor;
        }

        public string GetUserConnectionString()
        {
            if (!string.IsNullOrEmpty(connectionString)) return connectionString;

            var user = _httpContextAccessor.HttpContext.User;

            return user.Claims.Where(x => x.Type == "Cs").FirstOrDefault()?.Value;
        }

        public int GetUserId()
        {
            var user = _httpContextAccessor.HttpContext.User;

            return int.Parse(user.Claims.Where(x => x.Type == "Id").FirstOrDefault().Value);
        }

        public Platform GetUserPlatform()
        {
            var user = _httpContextAccessor.HttpContext.User;

            return EnumHelper.Parse<Platform>(user.Claims.Where(x => x.Type == "Platform").FirstOrDefault().Value);
        }

        public string GetVersion()
        {
            var user = _httpContextAccessor.HttpContext.User;

            return user.Claims.Where(x => x.Type == "Version").FirstOrDefault().Value ?? "1";
        }
    }
}
