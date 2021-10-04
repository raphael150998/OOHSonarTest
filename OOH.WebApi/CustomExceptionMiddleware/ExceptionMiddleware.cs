using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Logging;
using OOH.WebApi.Models;
using Serilog.Context;
using Serilog.Core;
using Serilog.Core.Enrichers;
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
                    string platform = user.Claims.Where(x => x.Type == "Platform").FirstOrDefault()?.Value ?? "";
                    string version = user.Claims.Where(x => x.Type == "Version").FirstOrDefault()?.Value ?? "";
                    string userId = user.Claims.Where(x => x.Type == "Id").FirstOrDefault()?.Value ?? "";

                    if (!string.IsNullOrEmpty(empresaId))
                    {
                        using (LogContext.PushProperty("Empresa", int.Parse(empresaId)))
                        {
                            LogContext.PushProperty("Platform", platform);
                            LogContext.PushProperty("Version", version);
                            LogContext.PushProperty("UserId", int.Parse(userId));
                            _logger.LogError(ex, $"Ha ocurrido un error no manejado para la empresa con identificador: {empresaId}. Mensaje de error: {ex.Message}. Ver Detalles en objeto 'Exception' de la base de bitacoras");
                        }
                    }

                }

                httpContext.Response.StatusCode = (int)HttpStatusCode.InternalServerError;
                await httpContext.Response.WriteAsync(ex.ToString());

            }
        }
    }
}
