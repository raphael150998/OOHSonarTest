using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using OOH.Data.Helpers;
using OOH.Data.Repos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using OOH.Data.Dtos.Usuario;

namespace OOH.WebApi.ApiControllers
{
  
    [ApiController]
    public class AccountApiController : ControllerBase
    {
        private static AccountRepository _Repository = new AccountRepository();

        [HttpPost]
        [Route("api/login/Validate")]
        public ResultClass Validate(UserLoginDto user)
        {
            return _Repository.ValidarLogin(user.Login,user.Pass);

        }

    }
}
