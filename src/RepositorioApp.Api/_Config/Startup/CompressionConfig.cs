using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.ResponseCompression;
using Microsoft.Extensions.DependencyInjection;
namespace RepositorioApp.Api._Config.Startup
{
    public static class CompressionConfig
    {
        public static IServiceCollection AppAddCompression(this IServiceCollection services)
        {
            services.AddResponseCompression(options =>
            {
                options.Providers.Add<BrotliCompressionProvider>();
                options.EnableForHttps = true;
            });

            return services;
        }
    }
}
