using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using OOH.Data.Helpers;
using OOH.Data.Repos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using OOH.Data.Dtos.Usuario;
using Microsoft.Extensions.Logging;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using System.Security.Claims;
using OOH.Data.Models;
using Newtonsoft.Json;
using Dapper;
using OOH.Data.Herlpers;
using OOH.Data;

namespace OOH.WebApi.ApiControllers
{

    [ApiController]
    public class AccountApiController : ControllerBase
    {
        private readonly AccountRepository _repo;
        private readonly ILogger _logger;

        public AccountApiController(ILogger<AccountApiController> logger, AccountRepository repo)
        {
            _logger = logger;
            _repo = repo;
        }

        [HttpPost]
        [Route("api/login/Validate")]
        public async Task<ResultClass> Validate([FromBody] UserLoginDto user)
        {
            var Login = _repo.ValidarLogin(user.Login, user.Pass);
            if (Login.state)
            {
                UserPermisoDto UserLoged = (UserPermisoDto)Login.data;
                string JsonListPermisos = JsonConvert.SerializeObject(UserLoged.Permisos);

                //Claim que tiene los datos de las cookies
                var claims = new List<Claim>
                {
                    new Claim(ClaimTypes.Name, UserLoged.User.Login),
                    new Claim("FullName", UserLoged.User.Username),
                    new Claim("Id", UserLoged.User.UserId.ToString()),
                    new Claim("Cs", UserLoged.StringConecction.ToString()),
                    new Claim("ListPermisos", JsonListPermisos),
                    new Claim("Language", UserLoged.User.Idioma.ToString()),
                    new Claim("Empresa", UserLoged.User.EmpresaId.ToString()),
                    new Claim("Version", "0.1"),//la version esta hardcode de momento, posteriormente se implementara versionamiento desde el appsettings o desde la url de los controllers
                    new Claim("Platform", Platform.Web.GetValueString())//la plataforma se encuentra de momento por default en web ya que falta implementar la parametrizacion de esta en los endpoints
                };

                var claimsIdentity = new ClaimsIdentity(
                    claims, CookieAuthenticationDefaults.AuthenticationScheme);

                var authProperties = new AuthenticationProperties
                {
                    IsPersistent = false,
                };

                await HttpContext.SignInAsync(
                   CookieAuthenticationDefaults.AuthenticationScheme,
                   new ClaimsPrincipal(claimsIdentity),
                   authProperties);
            }


            return Login;
        }
        [HttpPost]
        [Route("api/login/Logout")]
        public async void Logout()
        {
            await HttpContext.SignOutAsync(CookieAuthenticationDefaults.AuthenticationScheme);

        }
        [HttpPost]
        [Route("api/Account/Register")]
        public async Task<ResultClass> Register([FromForm] Usuarios registro)
        {
            return _repo.RegistroUser(registro);
        }
    }
}
