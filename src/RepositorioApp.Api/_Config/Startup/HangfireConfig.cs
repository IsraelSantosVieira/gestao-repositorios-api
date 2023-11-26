using System;
using Hangfire;
using Hangfire.AspNetCore;
using Hangfire.PostgreSql;
using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Npgsql;
using RepositorioApp.Api._Config.FiltersAndAttributes;
using RepositorioApp.CrossCutting.Config;
namespace RepositorioApp.Api._Config.Startup
{
    public static class HangfireConfig
    {
        public static IServiceCollection AppAddHangfire(
            this IServiceCollection services,
            IConfiguration config)
        {
            var sb = new NpgsqlConnectionStringBuilder(config.GetConnectionString("Default"))
            {
                Pooling = true,
                MaxPoolSize = 2
            };

            services.AddHangfire(opt =>
            {
                opt.UsePostgreSqlStorage(sb.ConnectionString);
            });

            services.AddHangfireServer(x =>
            {
                x.Activator = new AspNetCoreJobActivator(services.BuildServiceProvider().GetRequiredService<IServiceScopeFactory>());
                x.WorkerCount = 2;
            });

            JobStorage.Current = new PostgreSqlStorage(sb.ConnectionString);

            return services;
        }

        public static IApplicationBuilder UseCustomHangfire(this IApplicationBuilder app, IConfiguration config, IHostEnvironment env)
        {
            var dashboarOptions = new DashboardOptions
            {
                AppPath = config[$"{nameof(AppConfig)}:{nameof(AppConfig.AppUrl)}"],
                DisplayStorageConnectionString = false,
                AsyncAuthorization = new[]
                {
                    new HangfireDashboardFilterAttribute()
                }
            };

            app.UseHangfireDashboard("/jobs", dashboarOptions);

            return app;
        }

        public static IApplicationBuilder UseConfigureRecurringJobs(this IApplicationBuilder app, IHostEnvironment env)
        {
            var timezone = env.IsDevelopment()
                ? TimeZoneInfo.Local
                : TimeZoneInfo.FindSystemTimeZoneById("E. South America Standard Time");

            var manager = new RecurringJobManager();
            var recurringJobOptions = new RecurringJobOptions
            {
                TimeZone = timezone
            };

            /*manager.AddOrUpdate("ExampleJob",
                Job.FromExpression<IJobservice>(x => x.ExampleJob()),
                Cron.Daily(3),
                recurringJobOptions);*/

            return app;
        }
    }
}
