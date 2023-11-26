using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using RepositorioApp.Jwt.Contracts;
using RepositorioApp.Jwt.Models;
namespace RepositorioApp.Jwt
{
    public class JwtServiceDiscoveryMiddleware
    {
        private readonly RequestDelegate _next;

        public JwtServiceDiscoveryMiddleware(RequestDelegate next)
        {
            _next = next;
        }

        public async Task Invoke(HttpContext httpContext, ISecurityKeyStoreService securityKeyStoreService)
        {
            var keys = securityKeyStoreService.GetCurrents()
                .ToArray()
                .Select(x => CustomJsonSerializer.Deserialize<BaseECDsaData>(x.Parameters))
                .ToArray();
            await httpContext.Response.WriteAsync(CustomJsonSerializer.Serialize(new
            {
                keys
            }));
        }
    }
}
