using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using Microsoft.AspNetCore.Http;
using RepositorioApp.CrossCutting.Constants;
using RepositorioApp.Domain.Contracts.Infra;
namespace RepositorioApp.Infra.Security
{
    public class SessionProvider : ISessionProvider
    {
        private readonly IHttpContextAccessor _accessor;

        public SessionProvider(IHttpContextAccessor accessor)
        {
            _accessor = accessor;
        }

        public ClaimsPrincipal User => _accessor?.HttpContext?.User;

        public bool IsAuthenticated => _accessor?.HttpContext?.User?.Identity?.IsAuthenticated == true;

        public IEnumerable<Claim> Claims => User.Claims;

        public Guid Id
        {
            get
            {
                var value = User.Claims.FirstOrDefault(x => x.Type == CustomClaims.Id)?.Value ?? "";
                return Guid.TryParse(value, out var id)
                    ? id
                    : Guid.Empty;
            }
        }


        public string GetClientIp(HttpContext httpContext)
        {
            return httpContext.Connection.RemoteIpAddress?.ToString();
        }

        public string Email => User.Claims.FirstOrDefault(x => x.Type == CustomClaims.Email)?.Value;

        public string Name => User.Claims.FirstOrDefault(x => x.Type == CustomClaims.FullName)?.Value;

        public string Avatar => User.Claims.FirstOrDefault(x => x.Type == CustomClaims.Avatar)?.Value;
        
        public string Master => User.Claims.FirstOrDefault(x => x.Type == CustomClaims.Master)?.Value;
    }
}
