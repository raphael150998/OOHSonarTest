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
            services.AddScoped<AdvertisingAgencyRepo>();
            services.AddScoped<OOHContext>();
            services.AddScoped<AccountRepo>();
            services.AddScoped<ClientRepo>();
            services.AddScoped<AddressRepo>();
            services.AddScoped<MaterialRepo>();
            services.AddScoped<CaraMaterialRepo>();
            services.AddScoped<CategoryRepo>();
            services.AddScoped<QuotationRepo>();
            services.AddScoped<FaceRepo>();
            services.AddScoped<FacePriceRepo>();
            services.AddScoped<ContactsRepo>();
            services.AddScoped<SiteRepo>();
            services.AddScoped<ProviderRepo>();
            services.AddScoped<ZoneRepo>();
            services.AddScoped<CommercialReferencesRepo>();
            services.AddScoped<CommercialRestrictionsRepo>();
            services.AddScoped<InsuranceTypesRepo>();
            services.AddScoped<InsuranceSiteRepo>();
            services.AddScoped<PermissionSiteRepo>();
            services.AddScoped<PermissionTypesRepo>();
            services.AddScoped<SiteProviderRepo>();
            services.AddScoped<ReferenceSiteRepo>();
            services.AddScoped<RestrictionSiteRepo>();
            services.AddScoped<SiteCategoryRepo>();
            services.AddScoped<SiteElectricMeterRepo>();
            services.AddScoped<StateTypesRepo>();
            services.AddScoped<StructureTypesRepo>();
            services.AddScoped<CostSiteRepo>();
            services.AddScoped<CostCenterRepo>();
            services.AddScoped<AccessTimeRepo>();
            services.AddScoped<ContactsProviderRepo>();
            services.AddScoped<IMapGenerator, MapGenerator>();
            services.AddScoped<IPowerpointProvider, PowerpointProvider>();
            services.AddScoped<TypesRepo>();
            services.AddScoped<ContractFaceRepo>();
            services.AddScoped<ContractRepo>();
            services.AddScoped<IndustryRepo>();
            services.AddScoped<PromotionRepo>();
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
                //app.UseExceptionHandler("/Home/Error");
                app.UseStatusCodePagesWithReExecute("/StatusCode", "?code={0}");
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
