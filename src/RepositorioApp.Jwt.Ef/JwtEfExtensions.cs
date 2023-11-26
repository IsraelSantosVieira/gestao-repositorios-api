using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using RepositorioApp.Jwt.Contracts;
namespace RepositorioApp.Jwt.Ef
{
    public static class JwtConfigExtensions
    {
        public static IServiceCollection AddJwtEntityFramework<TContext>(this IServiceCollection services)
            where TContext : DbContext
        {
            services.AddScoped<ISecurityKeyStoreService, SecurityKeyStoreService<TContext>>();

            services.AddMemoryCache();

            return services;
        }
    }
}
