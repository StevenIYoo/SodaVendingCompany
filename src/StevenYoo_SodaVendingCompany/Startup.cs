using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Configuration;
using Newtonsoft.Json.Serialization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Authentication.Cookies;
using StevenYoo_SodaVendingCompany.Models;
using StevenYoo_SodaVendingCompany.Data;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;

namespace StevenYoo_SodaVendingCompany
{
    public class Startup
    {
        private IHostingEnvironment _env;
        private IConfigurationRoot _config;
        // This method gets called by the runtime. Use this method to add services to the container.
        // For more information on how to configure your application, visit http://go.microsoft.com/fwlink/?LinkID=398940

        public Startup(IHostingEnvironment env)
        {
            _env = env;

            var builder = new ConfigurationBuilder()
                .SetBasePath(_env.ContentRootPath)
                .AddJsonFile("config.json")
                .AddEnvironmentVariables();

            _config = builder.Build();
        }

        public void ConfigureServices(IServiceCollection services)
        {
            services.AddMvc(config =>
            {
                if (_env.IsProduction())
                {
                    //if (_env.IsProduction())
                    //{
                    //    config.Filters.Add(new RequireHttpsAttribute());
                    //}
                }
            })
            .AddJsonOptions(config =>
            {
                config.SerializerSettings.ContractResolver = new CamelCasePropertyNamesContractResolver();
            });

            services.AddIdentity<User, IdentityRole>(config =>
            {
                config.Password.RequiredLength = 8;
                config.Cookies.ApplicationCookie.LoginPath = "/Authorization/Login";
                config.Cookies.ApplicationCookie.Events = new CookieAuthenticationEvents()
                {
                    OnRedirectToLogin = async ctx =>
                    {
                        if (ctx.Request.Path.StartsWithSegments("/api") &&
                            ctx.Response.StatusCode == 200)
                        {
                            ctx.Response.StatusCode = 401;
                        }
                        else
                        {
                            ctx.Response.Redirect(ctx.RedirectUri);
                        }
                        await Task.Yield();
                    }
                };
            })
            .AddEntityFrameworkStores<UserContext>();

            services.AddSingleton(_config);
            services.AddScoped<IDatabaseRepository, DatabaseRepository>();

            services.AddScoped<IConsumerVendingMachineRepository, ConsumerVendingMachineRepository>();
            services.AddScoped<IConsumerVendingMachineData, ConsumerVendingMachineData>();

            services.AddScoped<IAdminVendingMachineRepository, AdminVendingMachineRepository>();
            services.AddScoped<IAdminVendingMachineData, AdminVendingMachineData>();

            services.AddScoped<IReportVendingMachineRepository, ReportVendingMachineRepository>();
            services.AddScoped<IReportVendingMachineData, ReportVendingMachineData>();

            services.AddDbContext<UserContext>();

        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IHostingEnvironment env, ILoggerFactory loggerFactory)
        {
            loggerFactory.AddConsole();

            if (env.IsEnvironment("Development"))
            {
                app.UseDeveloperExceptionPage();
                loggerFactory.AddDebug(LogLevel.Information);
            }
            else
            {
                loggerFactory.AddDebug(LogLevel.Error);
            }

            app.UseStaticFiles();

            app.UseIdentity();

            app.UseCookieAuthentication(new CookieAuthenticationOptions()
            {
                AuthenticationScheme = "CookieInstance",
                LoginPath = new PathString("/Authorization/Login"),
                AccessDeniedPath = new PathString("/Index"),
                AutomaticAuthenticate = true,
                AutomaticChallenge = true
            });

            app.UseMvc(config =>
            {
                config.MapRoute(
                    name: "Default",
                    template: "{controller}/{action}/{id?}",
                    defaults: new { controller = "App", action = "Index" }
                    );
            });
        }
    }
}
