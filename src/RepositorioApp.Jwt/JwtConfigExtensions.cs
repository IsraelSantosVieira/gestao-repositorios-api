using System;
using System.Net.Http;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Protocols;
using Microsoft.IdentityModel.Protocols.OpenIdConnect;
using RepositorioApp.Jwt.Contracts;
using RepositorioApp.Jwt.Services;
namespace RepositorioApp.Jwt
{
    public static class JwtConfigExtensions
    {
        public static IServiceCollection AddJwtAuthentication(this IServiceCollection services, Action<JwtOptions> options)
        {
            services.Configure(options);

            services.AddScoped<IJwtService, JwtService>();
            services.AddAuthentication(x =>
            {
                x.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
                x.DefaultScheme = JwtBearerDefaults.AuthenticationScheme;
                x.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
            }).AddJwtBearerConfig();

            services.AddMemoryCache();

            return services;
        }

        private static AuthenticationBuilder AddJwtBearerConfig(this AuthenticationBuilder builder)
        {
            var serviceProvider = builder.Services.BuildServiceProvider();

            var options = serviceProvider.GetRequiredService<IOptions<JwtOptions>>().Value;

            builder.AddJwtBearer(x =>
            {
                x.RequireHttpsMetadata = options.RequireHttps;
                x.SaveToken = true;
                x.IncludeErrorDetails = true;
                x.SetJwksOptions(options);
            });

            return builder;
        }

        public static void SetJwksOptions(this JwtBearerOptions options, JwtOptions jwkOptions)
        {
            var httpClient = new HttpClient(options.BackchannelHttpHandler ?? new HttpClientHandler())
            {
                Timeout = options.BackchannelTimeout,
                MaxResponseContentBufferSize = 1024 * 1024 * 10// 10 MB
            };

            options.ConfigurationManager = new ConfigurationManager<OpenIdConnectConfiguration>(
                jwkOptions.JwksUri.OriginalString,
                new JwksRetriever(),
                new HttpDocumentRetriever(httpClient)
                {
                    RequireHttps = jwkOptions.RequireHttps
                });
            options.TokenValidationParameters.ValidateAudience = false;
            options.TokenValidationParameters.ValidIssuer = jwkOptions.Issuer;
            options.TokenValidationParameters.ClockSkew = TimeSpan.FromMinutes(jwkOptions.TokenClockSkewMinutes);
        }

        public static IApplicationBuilder UseJwksDiscovery(this IApplicationBuilder app, string jwtDiscoveryEndpoint = "/jwks")
        {
            if (!jwtDiscoveryEndpoint.StartsWith('/')) throw new ArgumentException("The Jwks URI must starts with '/'");
            app.Map(new PathString(jwtDiscoveryEndpoint), x => x.UseMiddleware<JwtServiceDiscoveryMiddleware>());
            return app;
        }
    }
}
