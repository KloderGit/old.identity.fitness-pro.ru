using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using identity.fitness_pro.ru.Configuration;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Options;

namespace identity.fitness_pro.ru
{
    public class Startup
    {
        public IConfiguration Configuration { get; }
        public IConfiguration Settings { get; }
        public IHostingEnvironment Environment { get; }

        public Startup(IConfiguration configuration, IHostingEnvironment environment)
        {
            Configuration = configuration;
            Environment = environment;

            var cc = Configuration.GetSection("SettingsFilePath").Value;

            var builder = new ConfigurationBuilder()
                .SetBasePath(environment.ContentRootPath)
                .AddJsonFile(Configuration.GetSection("SettingsFilePath").Value + @"\clients.json", true, true)
                .AddJsonFile(Configuration.GetSection("SettingsFilePath").Value + @"\identityresources.json", true, true);
            Settings = builder.Build();

            //var builder = new ConfigurationBuilder()
            //    .SetBasePath(environment.ContentRootPath)
            //    .AddJsonFile("appsettings.json", optional: true, reloadOnChange: true)
            //    .AddJsonFile($"appsettings.{environment.EnvironmentName}.json", optional: true)
            //    .AddEnvironmentVariables()
            //    .AddJsonFile(Configuration.GetSection("SettingsFilePath").Value + @"\clients.json", true, true);
            //Configuration = builder.Build();
        }



        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            //services.Configure<CookiePolicyOptions>(options =>
            //{
            //    // This lambda determines whether user consent for non-essential cookies is needed for a given request.
            //    options.CheckConsentNeeded = context => true;
            //    options.MinimumSameSitePolicy = SameSiteMode.None;
            //});






            services.Configure<IEnumerable<IdentityResourceConfigItem>>(Settings.GetSection("CustomIdentityClaims"));







            services.AddMvc().SetCompatibilityVersion(CompatibilityVersion.Version_2_2);
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IHostingEnvironment env, IOptions<IdentityResourceConfigItem> options)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
            else
            {
                app.UseExceptionHandler("/Home/Error");
                // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
                app.UseHsts();
            }

            app.UseHttpsRedirection();
            app.UseStaticFiles();
            app.UseCookiePolicy();

            app.UseMvc(routes =>
            {
                routes.MapRoute(
                    name: "default",
                    template: "{controller=Home}/{action=Index}/{id?}");
            });
        }
    }
}
