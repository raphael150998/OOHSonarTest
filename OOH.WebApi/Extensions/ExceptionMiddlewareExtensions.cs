using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Diagnostics;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using OOH.WebApi.CustomExceptionMiddleware;
using OOH.WebApi.Models;
using Serilog.Context;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Security.Claims;
using System.Threading.Tasks;

namespace OOH.WebApi.Extensions
{
    public static class ExceptionMiddlewareExtensions
    {
        //public static void ConfigureExceptionHandler(this IApplicationBuilder app, ILogger<Startup> logger)
        //{
        //    //app.UseExceptionHandler(appError =>
        //    //{
        //    //    appError.Run(async context =>
        //    //    {
        //    //        //context.Response.StatusCode = (int)HttpStatusCode.InternalServerError;
        //    //        //context.Response.ContentType = "application/json";
        //    //        //var contextFeature = context.Features.Get<IExceptionHandlerFeature>();
        //    //        //if (contextFeature != null)
        //    //        //{
        //    //        //    logger.LogError($"Something went wrong: {contextFeature.Error}");
        //    //        //    await context.Response.WriteAsync(new ErrorDetails()
        //    //        //    {
        //    //        //        StatusCode = context.Response.StatusCode,
        //    //        //        Message = "Internal Server Error."
        //    //        //    }.ToString());
        //    //        //}

        //    //        if (context.User.Identity.IsAuthenticated)
        //    //        {
        //    //            ClaimsPrincipal user = context.User;

        //    //            string empresaId = user.Claims.Where(x => x.Type == "Empresa").FirstOrDefault()?.Value ?? "";

        //    //            if (!string.IsNullOrEmpty(empresaId))
        //    //            {
        //    //                using (LogContext.PushProperty("Empresa", int.Parse(empresaId)))
        //    //                {
        //    //                    logger.LogInformation($"error desde el middleWare para empresa {empresaId}");
        //    //                }
        //    //            }

        //    //        }
        //    //    });
        //    //});
        //}

        public static void ConfigureCustomExceptionMiddleware(this IApplicationBuilder app)
        {
            app.UseMiddleware<ExceptionMiddleware>();
        }
    }
}
