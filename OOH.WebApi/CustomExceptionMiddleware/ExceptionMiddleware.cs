using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Logging;
using OOH.WebApi.Models;
using Serilog.Context;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Security.Claims;
using System.Threading.Tasks;

namespace OOH.WebApi.CustomExceptionMiddleware
{
    public class ExceptionMiddleware
    {
        private readonly RequestDelegate _next;
        private readonly ILogger<Startup> _logger;
        public ExceptionMiddleware(RequestDelegate next, ILogger<Startup> logger)
        {
            _logger = logger;
            _next = next;
        }
        public async Task InvokeAsync(HttpContext httpContext)
        {
            try
            {
                await _next(httpContext);
            }
            catch (Exception ex)
            {
                if (httpContext.User.Identity.IsAuthenticated)
                {
                    ClaimsPrincipal user = httpContext.User;

                    string empresaId = user.Claims.Where(x => x.Type == "Empresa").FirstOrDefault()?.Value ?? "";

                    if (!string.IsNullOrEmpty(empresaId))
                    {
                        using (LogContext.PushProperty("Empresa", int.Parse(empresaId)))
                        {
                            _logger.LogError(ex, $"error desde el middleWare para empresa {empresaId}");
                        }
                    }

                } 

                httpContext.Response.StatusCode = (int)HttpStatusCode.InternalServerError;
            }
        }
    }
}
