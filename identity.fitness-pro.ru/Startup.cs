using identity.fitness_pro.ru.Configuration;
using identity.fitness_pro.ru.Configuration.Models;
using IdentityServer4.Models;
using IdentityServer4.Test;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Options;
using System;
using System.Collections.Generic;
using System.Linq;

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
            Settings = LoadExternalConfigurations(Environment.ContentRootPath);
        }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddMvc().SetCompatibilityVersion(CompatibilityVersion.Version_2_2);

            MapSettingToPoco(services);

            var identityOptions = GetConfigObject<IdetitySettingModel>(services);
            List<IdentityResource> identities = new List<IdentityResource>( IdentityConfig.GetIdentities(identityOptions.Identities) );
            identities.Add(new IdentityResources.OpenId());
            identities.Add(new IdentityResources.Profile());

            var apiOptions = GetConfigObject<ApiSettingModel>(services);
            var apies = ApiConfig.GetApis(apiOptions.Apies);

            var clientOptions = GetConfigObject<ClientSettingModel>(services);
            var clients = ClientsConfig.GetClients(clientOptions);

            var builder = services.AddIdentityServer()
                .AddInMemoryIdentityResources(identities)
                .AddInMemoryApiResources(apies)
                .AddInMemoryClients(clients)
                .AddTestUsers(Config.GetUsers());
            //.AddProfileService<CustomProfileService>();

            if (Environment.IsDevelopment())
            {
                builder.AddDeveloperSigningCredential();
            }
            else
            {
                throw new Exception("need to configure key material");
            }
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IHostingEnvironment env)
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
                .AddJsonFile(Configuration.GetSection("SettingsFilePath").Value + @"\ClientSettings.json", true, true);
            return builder.Build();
        }

        void MapSettingToPoco(IServiceCollection services)
        {
            services.Configure<ClientSettingModel>(Settings);
            services.Configure<ApiSettingModel>(Settings);
            services.Configure<IdetitySettingModel>(Settings);
        }

        T GetConfigObject<T>(IServiceCollection services) where T: class, new()
        {
            var serviceProvider = services.BuildServiceProvider();
            return serviceProvider.GetService<IOptions<T>>().Value;
        }
    }
}
