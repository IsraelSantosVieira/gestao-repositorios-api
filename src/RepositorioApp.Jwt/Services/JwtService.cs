using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.JsonWebTokens;
using Microsoft.IdentityModel.Tokens;
using RepositorioApp.Jwt.Contracts;
using RepositorioApp.Jwt.Models;
using SecurityKey=RepositorioApp.Jwt.Models.SecurityKey;
namespace RepositorioApp.Jwt.Services
{
    public class JwtService : IJwtService
    {
        private readonly ILogger<JwtService> _logger;
        private readonly JwtOptions _options;
        private readonly ISecurityKeyStoreService _securityKeyStoreService;

        public JwtService(
            ISecurityKeyStoreService securityKeyStoreService,
            IOptions<JwtOptions> options,
            ILogger<JwtService> logger)
        {
            _securityKeyStoreService = securityKeyStoreService;
            _logger = logger;
            _options = options.Value;
        }

        public async ValueTask<JwtWithUserData<T>> GenerateTokenAsync<T>(
            T userData,
            ClaimsIdentity identity,
            int? expirationMinutes = null,
            ClaimsIdentity identityForRefreshToken = null,
            int? refreshTokenExpirationMinutes = null)
        {
            return await GenerateTokenAsync(userData, identity.Claims, expirationMinutes, identityForRefreshToken?.Claims, refreshTokenExpirationMinutes);
        }

        public async ValueTask<JwtWithUserData<T>> GenerateTokenAsync<T>(
            T userData,
            IEnumerable<Claim> claims,
            int? expirationMinutes = null,
            IEnumerable<Claim> claimsForRefreshToekn = null,
            int? refreshTokenExpirationMinutes = null)
        {
            var jwtWithUserData = new JwtWithUserData<T>
            {
                UserData = userData
            };

            var securityKey = GetCurrentSecurityKey();

            var handler = new JwtSecurityTokenHandler();

            var jwt = GetJwtSecurityToken(claims, expirationMinutes ?? _options.TokenExpirationMinutes, securityKey);

            jwtWithUserData.AccessToken = handler.WriteToken(jwt);
            jwtWithUserData.ExpiresIn = jwt.ValidTo;
            jwtWithUserData.NotBefore = jwt.ValidFrom;

            claimsForRefreshToekn = claimsForRefreshToekn == null
                ? Array.Empty<Claim>()
                : claimsForRefreshToekn.ToArray();

            if (!claimsForRefreshToekn.Any())
                return jwtWithUserData;

            refreshTokenExpirationMinutes ??= _options.RefreshTokenExpirationMinutes;
            var jwtRefreshToken = GetJwtSecurityToken(claimsForRefreshToekn, refreshTokenExpirationMinutes, securityKey);
            jwtWithUserData.RefreshToken = handler.WriteToken(jwtRefreshToken);
            jwtWithUserData.RefreshTokenExpiresIn = jwtRefreshToken.ValidTo;
            return await ValueTask.FromResult(jwtWithUserData);
        }

        public async ValueTask<string> GenerateTokenAsync(ClaimsIdentity identity, int? expirationMinutes = null)
        {
            return await GenerateTokenAsync(identity.Claims, expirationMinutes);
        }

        public async ValueTask<string> GenerateTokenAsync(IEnumerable<Claim> claims, int? expirationMinutes = null)
        {
            var securityKey = GetCurrentSecurityKey();

            var jwt = GetJwtSecurityToken(claims, expirationMinutes, securityKey);

            return await ValueTask.FromResult(new JwtSecurityTokenHandler().WriteToken(jwt));
        }


        public async ValueTask<TokenValidationResult> ValidateTokenAsync(string token)
        {
            var result = new TokenValidationResult
            {
                IsValid = false
            };

            var handler = new JsonWebTokenHandler();

            var securityKeys = _securityKeyStoreService.GetCurrents();

            foreach (var securityKey in securityKeys)
            {
                result = await handler.ValidateTokenAsync(token.Replace("Bearer ", ""),
                    new TokenValidationParameters
                    {
                        ValidAudiences = new[]
                        {
                            _options.Audience
                        },
                        ValidIssuers = new[]
                        {
                            _options.Issuer
                        },
                        ValidateAudience = _options.ValidateAudience,
                        ValidateIssuer = _options.ValidateIssuer,
                        IssuerSigningKey = new ECDsaSecurityKey(securityKey.GetECDsa()),
                        ValidateLifetime = true,
                        ClockSkew = TimeSpan.FromMinutes(_options.TokenClockSkewMinutes)
                    });

                if (result.IsValid) break;
            }

            return await ValueTask.FromResult(result);
        }

        public ClaimsPrincipal ValidateToken(string token, out SecurityToken securityToken)
        {
            securityToken = new JwtSecurityToken();

            ClaimsPrincipal principal = null;

            var handler = new JwtSecurityTokenHandler();

            var securityKeys = _securityKeyStoreService.GetCurrents().ToList();

            foreach (var securityKey in securityKeys)
            {
                try
                {
                    var ecda = securityKey.GetECDsa();

                    principal = handler.ValidateToken(token.Replace("Bearer ", ""),
                        new TokenValidationParameters
                        {
                            ValidAudiences = new[]
                            {
                                _options.Audience
                            },
                            ValidIssuers = new[]
                            {
                                _options.Issuer
                            },
                            ValidateAudience = _options.ValidateAudience,
                            ValidateIssuer = _options.ValidateIssuer,
                            IssuerSigningKey = new ECDsaSecurityKey(ecda),
                            ValidateLifetime = true,
                            ClockSkew = TimeSpan.FromMinutes(_options.TokenClockSkewMinutes)
                        },
                        out securityToken);

                    if (principal?.Identity?.IsAuthenticated == true) break;
                }
                catch (Exception e)
                {
                    principal = null;
                    _logger.LogCritical(e, "Error on validate JWT");
                }
            }

            return principal ?? new ClaimsPrincipal();
        }


        public bool CanReadToken(string token)
        {
            return new JwtSecurityTokenHandler().CanReadToken(token);
        }

        private SecurityKey GetCurrentSecurityKey()
        {
            var securityKey = _securityKeyStoreService.GetCurrent();
            if (securityKey == null)
                throw new NullReferenceException("SecurityKey is null");
            return securityKey;
        }

        private JwtSecurityToken GetJwtSecurityToken(IEnumerable<Claim> claims, int? expirationMinutes, SecurityKey securityKey)
        {
            var jwtDate = DateTime.UtcNow;
            var jwt = new JwtSecurityToken(
                audience: _options.Audience,
                issuer: _options.Issuer,
                claims: claims,
                notBefore: jwtDate,
                expires: jwtDate.AddMinutes(expirationMinutes ?? _options.TokenExpirationMinutes),
                signingCredentials: securityKey.GetSigningCredentials()
            );
            return jwt;
        }
    }
}
