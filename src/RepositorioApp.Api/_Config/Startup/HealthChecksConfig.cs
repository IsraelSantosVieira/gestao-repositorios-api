using System;
using System.Linq;
using System.Net.Mime;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Diagnostics.HealthChecks;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Diagnostics.HealthChecks;
using Newtonsoft.Json;
using RepositorioApp.Api._Config.HealthChecks;
namespace RepositorioApp.Api._Config.Startup
{
    public static class HealthChecksConfig
    {
        public static IServiceCollection AppAddHealthChecks(this IServiceCollection services, IConfiguration config)
        {
            services.AddHealthChecks()
                .AddCheck("database",
                    new NpgSqlConnectionHealthCheck(config.GetConnectionString("Default")));

            return services;
        }

        public static IApplicationBuilder AppUseHealthChecks(this IApplicationBuilder app)
        {
            app.UseHealthChecks("/status",
                new HealthCheckOptions
                {
                    ResponseWriter = async (context, report) =>
                    {
                        var result = JsonConvert.SerializeObject(
                            new
                            {
                                api_status = report.Status.ToString(),
                                health_checks = report.Entries.Select(e => new
                                {
                                    check = e.Key,
                                    status = Enum.GetName(typeof(HealthStatus), e.Value.Status)
                                })
                            });
                        context.Response.ContentType = MediaTypeNames.Application.Json;
                        await context.Response.WriteAsync(result);
                    }
                });

            return app;
        }
    }
}
