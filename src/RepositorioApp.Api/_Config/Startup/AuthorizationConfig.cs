using System;
using System.IdentityModel.Tokens.Jwt;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using RepositorioApp.CrossCutting.Config;
using RepositorioApp.CrossCutting.Constants;
using RepositorioApp.Data;
using RepositorioApp.Extensions;
using RepositorioApp.Jwt;
using RepositorioApp.Jwt.Ef;
namespace RepositorioApp.Api._Config.Startup
{
    public static class AuthorizationConfig
    {
        public static IServiceCollection AppAddAuthorization(this IServiceCollection services,
            IConfiguration config, IHostEnvironment env)
        {
            var jwtTokenConfig = new JwTokenConfig();
            config.GetSection(nameof(JwTokenConfig)).Bind(jwtTokenConfig);

            var appConfig = new AppConfig();
            config.GetSection(nameof(AppConfig)).Bind(appConfig);

            JwtSecurityTokenHandler.DefaultInboundClaimTypeMap.Clear();

            services.AddJwtAuthentication(x =>
            {
                x.Audience = jwtTokenConfig.Audience;
                x.Issuer = jwtTokenConfig.Issuer;
                x.RequireHttps = appConfig.RequireHttps;
                x.ValidateAudience = true;
                x.ValidateIssuer = true;
                x.KeyExpirationDays = 7;
                x.TokenExpirationMinutes = jwtTokenConfig.ExpiresIn;
                x.RefreshTokenExpirationMinutes = jwtTokenConfig.RefreshTokenDaysExpiration * 24 * 60;
                x.JwksUri = new Uri(appConfig.BaseUrl.AddPath("jwks"));
            });

            services.AddJwtEntityFramework<DataContext>();


            services.AddAuthorization(auth =>
            {
                auth.AddPolicy(AppPolices.Authenticated, policy =>
                {
                    policy.RequireAuthenticatedUser()
                        .Build();
                });
                
                auth.AddPolicy(AppPolices.Master, policy =>
                {
                    policy.RequireAuthenticatedUser()
                        .RequireClaim(CustomClaims.Master)
                        .Build();
                });

                auth.AddPolicy(AppPolices.ExternalAccess, policy =>
                {
                    policy.RequireAuthenticatedUser()
                        .RequireClaim(CustomClaims.Id)
                        .Build();
                });
            });

            return services;
        }
    }
}
