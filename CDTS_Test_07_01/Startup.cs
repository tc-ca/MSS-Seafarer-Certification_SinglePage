using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using GoC.WebTemplate.Components.Core.Services;
using CDTS_Test_07_01.Middleware;
using AKSoftware.Localization.MultiLanguages;
using System.Reflection;
using System.Globalization;
using Microsoft.AspNetCore.Localization;
using CDTS_Test_07_01.Services;



namespace CDTS_Test_07_01
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        // For more information on how to configure your application, visit https://go.microsoft.com/fwlink/?LinkID=398940
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddModelAccessor();
            services.ConfigureGoCTemplateRequestLocalization(); // >= v2.1.1
            // ConfigureGoCTemplateRequestLocalization

            services.AddRazorPages();
            services.AddServerSideBlazor();
            services.AddSingleton<UserPreference>();

            // following part add language/culture support to CDTS template
            var supportedCultures = new List<CultureInfo> { new CultureInfo("en-CA"), new CultureInfo("fr-CA") };
            services.Configure<RequestLocalizationOptions>(options =>
            {
                options.DefaultRequestCulture = new Microsoft.AspNetCore.Localization.RequestCulture("en-CA");
                options.SupportedUICultures = supportedCultures;
                options.SupportedCultures = supportedCultures;
                options.RequestCultureProviders.Clear();
                options.RequestCultureProviders.Add(new Services.CustomRequestCultureProvider());
            });
            services.AddLanguageContainer(Assembly.GetExecutingAssembly(), CultureInfo.GetCultureInfo("en-CA"));// this is added for ILanguageContainerService
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
            else
            {
                app.UseExceptionHandler("/Error");
            }

            app.UsePageSettingsMiddleware();
            app.UseRequestLocalization(); // >= v2.1.1
            //app.UseRequestLocalization(CultureConfiguration.GetLocalizationOptions()); // <= v2.1.0

            app.UseStaticFiles();
            app.UseRouting();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapBlazorHub();
                endpoints.MapFallbackToPage("/_Host");
            });
        }
    }
}
