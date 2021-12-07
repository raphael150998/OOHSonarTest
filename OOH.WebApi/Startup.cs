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
using GoogleMapGenerator.Inteface;
using GoogleMapGenerator.Provider;
using PowerPointProvider.Provider;
using PowerPointProvider.Interface;

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
            services.AddScoped<AddressRepository>();
            services.AddScoped<MaterialRepository>();
            services.AddScoped<CaraMaterialRepository>();
            services.AddScoped<CategoryRepository>();
            services.AddScoped<QuotationRepository>();
            services.AddScoped<FaceRepository>();
            services.AddScoped<ContactsRepository>();
            services.AddScoped<SiteRepository>();
            services.AddScoped<ProviderRepository>();
            services.AddScoped<ZoneRepository>();
            services.AddScoped<CommercialReferencesRepository>();
            services.AddScoped<CommercialRestrictionsRepository>();
            services.AddScoped<InsuranceTypesRepository>();
            services.AddScoped<InsuranceSiteRepository>();
            services.AddScoped<PermissionSiteRepository>();
            services.AddScoped<PermissionTypesRepository>();
            services.AddScoped<SiteProviderRepository>();
            services.AddScoped<ReferenceSiteRepository>();
            services.AddScoped<RestrictionSiteRepository>();
            services.AddScoped<SiteCategoryRepository>();
            services.AddScoped<SiteElectricMeterRepository>();
            services.AddScoped<StateTypesRepository>();
            services.AddScoped<StructureTypesRepository>();
            services.AddScoped<CostSiteRepository>();
            services.AddScoped<CostCenterRepository>();
            services.AddScoped<AccessTimeRepository>();
            services.AddScoped<ContactsProviderRepository>();
            services.AddScoped<IMapGenerator, MapGenerator>();
            services.AddScoped<IPowerpointProvider, PowerpointProvider>();
            services.AddScoped<TypesRepository>();
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
