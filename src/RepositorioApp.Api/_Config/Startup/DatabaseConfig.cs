using System.Linq;
using Dapper;
using Microsoft.AspNetCore.Builder;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Npgsql;
using RepositorioApp.Data;
using RepositorioApp.Data.Extensions;
namespace RepositorioApp.Api._Config.Startup
{
    public static class DatabaseConfig
    {
        public static IServiceCollection AppAddDatabase(this IServiceCollection services,
            IConfiguration config, IHostEnvironment env)
        {
            services.AddDbContextPool<DataContext, DataContext>(options =>
            {
                options.UseCustomNpgsql(config.GetConnectionString("Default"));

                if (env.IsDevelopment())
                    options.EnableSensitiveDataLogging();
            });

            return services;
        }

        public static IApplicationBuilder AppEnsureMigrations(this IApplicationBuilder app,
            IHostEnvironment env)
        {
            using var serviceScope = app.ApplicationServices.CreateScope();

            var context = serviceScope.ServiceProvider.GetService<DataContext>();

            if (context == null) return app;

            InstallExtension(context);

            if (context.DbContext.Database.GetPendingMigrations().Any())
                context.DbContext.Database.Migrate();

            if (!env.IsProduction()) SeedDev.SeedEnvDev(context.DbContext, env.IsDevelopment());
            SeedDev.SeedEnvProd(context.DbContext);

            return app;
        }

        private static void InstallExtension(DataContext ctx)
        {
            using var conn = new NpgsqlConnection(ctx.Database.GetConnectionString());
            conn.Open();
            conn.Execute($"set schema '{DataContext.Schema}'; create extension if not exists unaccent");
            conn.Close();
        }
    }
}
