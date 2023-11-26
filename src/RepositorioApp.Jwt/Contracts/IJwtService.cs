using System.Collections.Generic;
using System.Security.Claims;
using System.Threading.Tasks;
using Microsoft.IdentityModel.Tokens;
using RepositorioApp.Jwt.Models;
namespace RepositorioApp.Jwt.Contracts
{
    public interface IJwtService
    {
        ValueTask<JwtWithUserData<T>> GenerateTokenAsync<T>(
            T userData,
            ClaimsIdentity identity,
            int? expirationMinutes = null,
            ClaimsIdentity identityForRefreshToken = null,
            int? refreshTokenExpirationMinutes = null);
        ValueTask<JwtWithUserData<T>> GenerateTokenAsync<T>(
            T userData,
            IEnumerable<Claim> claims,
            int? expirationMinutes = null,
            IEnumerable<Claim> claimsForRefreshToekn = null,
            int? refreshTokenExpirationMinutes = null);
        ValueTask<string> GenerateTokenAsync(ClaimsIdentity identity, int? expirationMinutes = null);
        ValueTask<string> GenerateTokenAsync(IEnumerable<Claim> claims, int? expirationMinutes = null);
        ValueTask<TokenValidationResult> ValidateTokenAsync(string token);
        ClaimsPrincipal ValidateToken(string token, out SecurityToken securityToken);
        bool CanReadToken(string token);
    }
}
