using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using OOH.Data.Repos;
using OOH.Data.Interfaces;
using System;
using Microsoft.AspNetCore.Authentication.Cookies;
using OOH.Data;
using Microsoft.Extensions.Logging;
using OOH.WebApi.Extensions;

namespace OOH.WebApi
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddAutoMapper(AppDomain.CurrentDomain.GetAssemblies());
            services.AddHttpContextAccessor();
            services.AddScoped<IWebUserHelper, WebUserHelper>();
            services.AddScoped<ILogHelper, LogHelper>();
            services.AddScoped<AdvertisingAgencyRepository>();
            services.AddScoped<OOHContext>();
            services.AddScoped<AccountRepository>();
            services.AddScoped<ClientRepository>();
            services.AddScoped<MunicipalityRepository>();
            services.AddScoped<CategoryRepository>();
            services.AddScoped<ContactsRepository>();
            services.AddControllersWithViews();
            services.AddCors();
            services.AddAuthentication(CookieAuthenticationDefaults.AuthenticationScheme)
            .AddCookie();
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env, ILogger<Startup> logger)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
                //app.UseStatusCodePagesWithReExecute("/StatusCode", "?code={0}");
            }
            else
            {
                app.UseExceptionHandler("/Home/Error");
                // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
                app.UseHsts();
            }

            //app.ConfigureExceptionHandler(logger);

            app.ConfigureCustomExceptionMiddleware();

            app.UseHttpsRedirection();
            app.UseStaticFiles();

            app.UseRouting();

            app.UseAuthentication();
            app.UseAuthorization();


            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllerRoute(
                    name: "default",
                    pattern: "{controller=Home}/{action=Index}/{id?}");

            });

        }
    }
}
