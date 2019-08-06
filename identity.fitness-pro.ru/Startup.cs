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
        public IConfiguration IdentityServerResourceConfig { get; }
        public IHostingEnvironment Environment { get; }
        public ILoggerFactory LoggerFactory { get; }
        private string externalConfigPath;
        private ExternalPrivateConfigBuilder privateConfig;

        public Startup(IConfiguration configuration, IHostingEnvironment environment)
        {
            Configuration = configuration;
            Environment = environment;
            externalConfigPath = Configuration.GetSection("SettingsFilePath").Value;
            privateConfig = new ExternalPrivateConfigBuilder(externalConfigPath);
            IdentityServerResourceConfig = privateConfig.GetConfiguration();
            //var commandLineArg = Configuration.GetValue<string>("build");
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

            var apiOptions = services.GetOption<ApiSettingModel>();
            var clientOptions = services.GetOption<ClientSettingModel>();
            var identityOptions = services.GetOption<IdetitySettingModel>();
            var identities = IdentityConfig.GetIdentities(identityOptions.Identities);

            var builder = services.AddIdentityServer()
                .AddInMemoryIdentityResources(identities)
                .AddInMemoryApiResources(ApiConfig.GetApis(apiOptions.Apies))
                .AddInMemoryClients(ClientsConfig.GetClients(clientOptions))
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
