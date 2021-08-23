﻿using Microsoft.AspNetCore.Http;
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

namespace OOH.WebApi.ApiControllers
{
  
    [ApiController]
    public class AccountApiController : ControllerBase
    {
        private static AccountRepository _Repository = new AccountRepository();
        private readonly ILogger _logger;

        public AccountApiController(ILogger<AccountApiController> logger)
        {
            _logger = logger;
        }

        [HttpPost]
        [Route("api/login/Validate")]
        public async Task<ResultClass> Validate([FromForm]UserLoginDto user)
        {
            var Login = _Repository.ValidarLogin(user.Login, user.Pass);
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
                    new Claim("ListPermisos", JsonListPermisos),
                };

                var claimsIdentity = new ClaimsIdentity(
                    claims, CookieAuthenticationDefaults.AuthenticationScheme);

                var authProperties = new AuthenticationProperties
                {
                    IsPersistent = true,
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
    }
}
