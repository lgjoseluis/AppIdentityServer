using IdentityServer4.EntityFramework.DbContexts;
using IdentityServer4.EntityFramework.Mappers;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using System;
using System.Linq;
using System.Reflection;

namespace IdentityServer
{
    public class Startup
    {
        public IConfiguration Configuration { get; }

        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        
        // This method gets called by the runtime. Use this method to add services to the container.
        // For more information on how to configure your application, visit https://go.microsoft.com/fwlink/?LinkID=398940
        public void ConfigureServices(IServiceCollection services)
        {
            string migrationsAssembly = typeof(Startup).GetTypeInfo().Assembly.GetName().Name;
            string connectionString = Configuration.GetConnectionString("AppDb");

            Action<SqlServerDbContextOptionsBuilder> sqlServerOptionsAction = sql => sql.MigrationsAssembly(migrationsAssembly);
            Action<DbContextOptionsBuilder> dbContext = ctx => ctx.UseSqlServer(connectionString, sqlServerOptionsAction);

            services.AddIdentityServer(
                options=> {
                    options.Events.RaiseErrorEvents = true;
                    options.Events.RaiseInformationEvents = true;
                    options.Events.RaiseFailureEvents = true;
                    options.Events.RaiseSuccessEvents = true;
                })
            .AddTestUsers(Config.GetTestUsers())
            .AddDeveloperSigningCredential()
            .AddConfigurationStore(options =>
            {
                options.ConfigureDbContext = dbContext;
            })
            .AddOperationalStore(options =>
            {
                options.ConfigureDbContext = dbContext;
            });
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            //SeedIdentityServerData(app); //Remove for production

            app.UseIdentityServer();
        }

        //private void SeedIdentityServerData(IApplicationBuilder app)
        //{
        //    var scope = app.ApplicationServices.GetService <IServiceScopeFactory>().CreateScope();

        //    var configDbCtx = scope.ServiceProvider.GetRequiredService<ConfigurationDbContext>();

        //    if (!configDbCtx.IdentityResources.Any())
        //    {
        //        foreach (var r in Config.GetIdentityResources())
        //        {
        //            configDbCtx.IdentityResources.Add(r.ToEntity());
        //        }
        //        configDbCtx.SaveChanges();
        //    }

        //    if (!configDbCtx.ApiResources.Any())
        //    {
        //        foreach (var r in Config.GetApiResources())
        //        {
        //            configDbCtx.ApiResources.Add(r.ToEntity());
        //        }
        //        configDbCtx.SaveChanges();
        //    }

        //    if (!configDbCtx.ApiScopes.Any())
        //    {
        //        foreach (var s in Config.GetApiScopes())
        //        {
        //            configDbCtx.ApiScopes.Add(s.ToEntity());
        //        }
        //        configDbCtx.SaveChanges();
        //    }

        //    if (!configDbCtx.Clients.Any())
        //    {
        //        foreach (var c in Config.GetClients())
        //        {
        //            configDbCtx.Clients.Add(c.ToEntity());
        //        }
        //        configDbCtx.SaveChanges();
        //    }
        //}
    }
}