using identity.fitness_pro.ru.Configuration;
using identity.fitness_pro.ru.Configuration.Extensions;
using identity.fitness_pro.ru.Configuration.Models;
using identity.fitness_pro.ru.Models;
using IdentityServer4.Models;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using Serilog;
using System;
using System.Collections.Generic;
using System.IO;
using System.Security.Cryptography.X509Certificates;

namespace identity.fitness_pro.ru
{
    public class Startup
    {
        public IConfiguration Configuration { get; }
        public IConfiguration Settings { get; }
        public IHostingEnvironment Environment { get; }
        public ILoggerFactory LoggerFactory { get; }
        private string externalConfigPath;
        private ExternalPrivateConfigBuilder privateConfig;

        public Startup(IConfiguration configuration, IHostingEnvironment environment)
        {
            Configuration = configuration;
            Environment = environment;
            externalConfigPath = Configuration.GetSection("SettingsFilePath").Value;
            //var commandLineArg = Configuration.GetValue<string>("build");
            privateConfig = new ExternalPrivateConfigBuilder(externalConfigPath);
            Settings = privateConfig.GetConfiguration();
        }

        public void ConfigureServices(IServiceCollection services)
        {
            privateConfig.MapToPocoInService(services);

            services.AddLogging();

            services.AddTransient<IConfiguration>(provider => Configuration);

            services.AddDbContext<ApplicationContext>(options => options.UseNpgsql(Configuration.GetConnectionString("PostgreSQL")));

            services.AddDefaultIdentity<ApplicationUser>()
                .AddRoles<IdentityRole>()
                .AddEntityFrameworkStores<ApplicationContext>();

            services.Configure<CertificatConfigModel>(Configuration.GetSection("Certificat"));
            var certificatConfigModel = services.GetOption<CertificatConfigModel>();


            var identityOptions = services.GetOption<IdetitySettingModel>();
            List<IdentityResource> identities = new List<IdentityResource>(IdentityConfig.GetIdentities(identityOptions.Identities))
            {
                new IdentityResources.OpenId(),
                new IdentityResources.Email(),
                new IdentityResources.Phone(),
                new IdentityResources.Profile()
            };

            var apiOptions = services.GetOption<ApiSettingModel>();
            var apies = ApiConfig.GetApis(apiOptions.Apies);

            var clientOptions = services.GetOption<ClientSettingModel>();
            var clients = ClientsConfig.GetClients(clientOptions);

            var builder = services.AddIdentityServer()
                .AddInMemoryIdentityResources(identities)
                .AddInMemoryApiResources(apies)
                .AddInMemoryClients(clients)
                .AddAspNetIdentity<ApplicationUser>()
                .AddCertificat(Environment.IsDevelopment(), certificatConfigModel);
            //.AddProfileService<CustomProfileService>();

            services.AddMvc().SetCompatibilityVersion(CompatibilityVersion.Version_2_2);
        }

        public void Configure(IApplicationBuilder app, IHostingEnvironment env, ILoggerFactory loggerFactory)
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
            app.UseAuthentication();

            app.UseIdentityServer();

            app.UseMvc(routes =>
            {
                routes.MapRoute(
                    name: "default",
                    template: "{controller=Home}/{action=Index}/{id?}");
            });
        }
    }
}
