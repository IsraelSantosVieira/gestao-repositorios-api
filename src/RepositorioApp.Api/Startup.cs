using System;
using System.Collections.Generic;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.HttpOverrides;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using RepositorioApp.Api._Config.Extensions;
using RepositorioApp.Api._Config.Startup;
using RepositorioApp.CrossCutting.Config;
using RepositorioApp.Jwt;
using RepositorioApp.Mail;
namespace RepositorioApp.Api
{
    public class Startup
    {
        public Startup(IConfiguration configuration,
            IHostEnvironment env)
        {
            var builder = new ConfigurationBuilder()
                .SetBasePath(env.ContentRootPath)
                .HandleAppSettings(env)
                .AddConfiguration(configuration)
                .AddEnvironmentVariables();
            
            Configuration = builder.Build();
            Env = env;
        }
        
        private IConfiguration Configuration { get; }
        private IHostEnvironment Env { get; }

        public void ConfigureServices(IServiceCollection services)
        {
            Configuration.ConfigureEnvVariables<AppConfig>(services)
                .ConfigureEnvVariables<JwTokenConfig>(services)
                .ConfigureEnvVariables<EmailConfig>(services)
                .ConfigureEnvVariables<AzureBlobConfig>(services);

            services
                .AppAddCompression()
                .AddHttpClient()
                .AddCors()
                .AppAddIoCServices(Configuration, Env)
                .AppAddMvc()
                .AppAddAuthorization(Configuration, Env)
                .AppAddApiDocs(Configuration)
                .AppAddHangfire(Configuration)
                .AppAddHealthChecks(Configuration)
                .AppAddDatabase(Configuration, Env)
                .AddLogger(Configuration);

            if (!Env.IsDevelopment())
            {
                services.AddHttpsRedirection(options =>
                {
                    options.RedirectStatusCode = StatusCodes.Status308PermanentRedirect;
                    options.HttpsPort = 443;
                });
            }
        }

        public void Configure(IApplicationBuilder app, IHostEnvironment env, IServiceProvider serviceProvider, ILoggerFactory loggerFactory)
        {
            var forwardOptions = new ForwardedHeadersOptions
            {
                ForwardedHeaders = ForwardedHeaders.XForwardedFor | ForwardedHeaders.XForwardedProto,
                RequireHeaderSymmetry = false
            };

            forwardOptions.KnownNetworks.Clear();
            forwardOptions.KnownProxies.Clear();

            app.UseForwardedHeaders(forwardOptions);

            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
            else
            {
                app.UseHsts();
                app.UseHttpsRedirection();
            }

            app.UseResponseHeadres(toAdd: new Dictionary<string, string>
            {
                {
                    "Content-Security-Policy", "frame-ancestors 'none'"
                },
                {
                    "X-Content-Type-Options", "nosniff"
                },
                {
                    "X-Frame-Options", "DENY"
                }
            });

            app.UseJwksDiscovery();
            app.AppUseHealthChecks();
            app.UseCors(x => x
                .WithOrigins(
                    Configuration.GetSection($"{nameof(AppConfig)}:{nameof(AppConfig.AppUrl)}").Value
                )
                .AllowCredentials()
                .AllowAnyMethod()
                .AllowAnyHeader()
                .SetIsOriginAllowedToAllowWildcardSubdomains());
            app.UseCookiePolicy();
            app.UseResponseCompression();
            app.UseStaticFiles();
            app.UsePathBase(Configuration.GetSection("AppConfig:BasePath").Value);
            app.AppUseApiDocs(Configuration);
            app.UseAuthentication();
            app.UseRouting();
            app.UseAuthorization();
            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
                endpoints.MapRazorPages();
            });
            app.UseCustomHangfire(Configuration, env);
            app.UseConfigureRecurringJobs(env);
            app.AppEnsureMigrations(env);
        }
    }
}
