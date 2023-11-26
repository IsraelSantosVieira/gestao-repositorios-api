using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Hosting;
namespace RepositorioApp.Api._Config.Extensions
{
    public static class ConfigurationBuilderExtensions
    {
        public static IConfigurationBuilder HandleAppSettings(this IConfigurationBuilder builder, IHostEnvironment env)
        {
            builder.AddJsonFile("secrets/appsettings.secrets.json", true);

            if (env.IsDevelopment())
            {
                builder.AddJsonFile("appsettings.Development.json", true, true);
                return builder;
            }
            if (env.IsStaging())
            {
                builder.AddJsonFile("appsettings.Staging.json", true, true);
                return builder;
            }
            builder.AddJsonFile("appsettings.json", true, true);
            return builder;
        }
    }
}
