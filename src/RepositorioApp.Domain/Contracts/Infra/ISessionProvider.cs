using System;
using System.Collections.Generic;
using System.Security.Claims;
using Microsoft.AspNetCore.Http;
namespace RepositorioApp.Domain.Contracts.Infra
{
    public interface ISessionProvider
    {
        public Guid Id { get; }
        public bool IsAuthenticated { get; }
        public IEnumerable<Claim> Claims { get; }
        public string Email { get; }
        public string Name { get; }
        public string Avatar { get; }
        public string Master { get; }
        public string GetClientIp(HttpContext httpContext);
    }


}
