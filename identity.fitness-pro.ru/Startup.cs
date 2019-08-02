using identity.fitness_pro.ru.Configuration;
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

namespace identity.fitness_pro.ru
{
    public class Startup
    {
        public IConfiguration Configuration { get; }
        public IConfiguration Settings { get; }
        public IHostingEnvironment Environment { get; }
        public ILoggerFactory LoggerFactory { get; }

        private string privateSettingPath;

        public Startup(IConfiguration configuration, IHostingEnvironment environment)
        {
            Configuration = configuration;
            Environment = environment;
            privateSettingPath = Configuration.GetSection("ConfigPath").Value;
            //var dfg = LoadExternalConfigurations(Environment.ContentRootPath);
            Settings = AppExternalSetting.LoadSettings(privateSettingPath);
        }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            MapSettingToPoco(services);

            services.AddLogging();

            var identityOptions = GetConfigObject<IdetitySettingModel>(services);
            List<IdentityResource> identities = new List<IdentityResource>(IdentityConfig.GetIdentities(identityOptions.Identities))
            {
                new IdentityResources.OpenId(),
                new IdentityResources.Email(),
                new IdentityResources.Phone(),
                new IdentityResources.Profile()
            };

            var apiOptions = GetConfigObject<ApiSettingModel>(services);
            var apies = ApiConfig.GetApis(apiOptions.Apies);
            var clientOptions = GetConfigObject<ClientSettingModel>(services);
            var clients = ClientsConfig.GetClients(clientOptions);

            var connectionOptions = GetConfigObject<ConnectionStringModel>(services);
            var connectionString = connectionOptions.ConnectionStrings["PostgreSQL"];

            if (Environment.IsDevelopment())
            {
                services.AddDbContext<ApplicationContext>(options => options.UseNpgsql(Configuration.GetConnectionString("PostgreSQL")));
            }
            else
            {
                services.AddDbContext<ApplicationContext>(options => options.UseNpgsql(connectionString));
            }

            services.AddDefaultIdentity<ApplicationUser>()
                .AddRoles<IdentityRole>()
                .AddEntityFrameworkStores<ApplicationContext>();

            var builder = services.AddIdentityServer()
                .AddInMemoryIdentityResources(identities)
                .AddInMemoryApiResources(apies)
                .AddInMemoryClients(clients)
                .AddAspNetIdentity<ApplicationUser>();
            //.AddProfileService<CustomProfileService>();

            if (Environment.IsDevelopment())
            {
                builder.AddDeveloperSigningCredential();
            }

            services.AddMvc().SetCompatibilityVersion(CompatibilityVersion.Version_2_2);
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
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

        IConfiguration LoadExternalConfigurations(string path)
        {
            var builder = new ConfigurationBuilder()
                .SetBasePath(path)
                .AddJsonFile(Configuration.GetSection("SettingsFilePath").Value + @"\IdentitySettings.json", true, true)
                .AddJsonFile(Configuration.GetSection("SettingsFilePath").Value + @"\ApiSettings.json", true, true)
                .AddJsonFile(Configuration.GetSection("SettingsFilePath").Value + @"\ClientSettings.json", true, true)
                .AddJsonFile(Configuration.GetSection("SettingsFilePath").Value + @"\ConnectionSettings.json", true, true);
            var sdfs= builder.Build();
            return sdfs;
        }

        void MapSettingToPoco(IServiceCollection services)
        {
            services.Configure<ClientSettingModel>(Settings);
            services.Configure<ApiSettingModel>(Settings);
            services.Configure<IdetitySettingModel>(Settings);
            services.Configure<ConnectionStringModel>(Settings);
        }

        T GetConfigObject<T>(IServiceCollection services) where T: class, new()
        {
            var serviceProvider = services.BuildServiceProvider();
            return serviceProvider.GetService<IOptions<T>>().Value;
        }
    }
}
