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
using System.Collections.Generic;

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
            services.Configure<IdentityResourceConfig>(Settings);
            services.Configure<ApiResourceConfig>(Settings);
            services.Configure<ClientResourceConfig>(Settings);

            var serviceProvider = services.BuildServiceProvider();

            var builder = services.AddIdentityServer()
                .AddInMemoryIdentityResources(new ResourceCreator<IdentityResourceConfig>().GetResources<IdentityResource>(serviceProvider.GetService<IOptions<IdentityResourceConfig>>()))
                .AddInMemoryApiResources(new ResourceCreator<ApiResourceConfig>().GetResources<ApiResource>(serviceProvider.GetService<IOptions<ApiResourceConfig>>()))
                .AddInMemoryClients(serviceProvider.GetService<IOptions<ClientResourceConfig>>().Value.GetPayload())
                .AddTestUsers( new TestUsers().GetUsers() );


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
                .AddJsonFile(Configuration.GetSection("SettingsFilePath").Value + @"\identityresources.json", true, true)
                .AddJsonFile(Configuration.GetSection("SettingsFilePath").Value + @"\apiresources.json", true, true)
                .AddJsonFile(Configuration.GetSection("SettingsFilePath").Value + @"\clients.json", true, true)
                .AddJsonFile(Configuration.GetSection("SettingsFilePath").Value + @"\testusers.json", true, true);
            return builder.Build();
        }
    }
}
