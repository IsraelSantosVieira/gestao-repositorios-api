using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
namespace RepositorioApp.Mail
{
    public static class MailServiceExtensions
    {
        public static IServiceCollection AddRepositorioAppMail(
            this IServiceCollection services,
            IConfiguration configuration,
            ServiceLifetime lifetime = ServiceLifetime.Singleton)
        {
            services.ConfigureMailService(lifetime, configuration.GetSection(nameof(EmailConfig)).Get<EmailConfig>());
            return services;
        }

        public static IServiceCollection AddRepositorioAppMail(
            this IServiceCollection services,
            bool enabableSsl,
            string userName,
            string password,
            string from,
            string fromName,
            string host,
            int port,
            ServiceLifetime lifetime = ServiceLifetime.Singleton
        )
        {
            services.ConfigureMailService(lifetime,
                new EmailConfig
                {
                    EnableSsl = enabableSsl,
                    UserName = userName,
                    Password = password,
                    From = from,
                    FromName = fromName,
                    Host = host,
                    Port = port
                });
            return services;
        }
        private static void ConfigureMailService(
            this IServiceCollection services,
            ServiceLifetime lifetime,
            EmailConfig emailConfig)
        {
            services.AddSingleton(emailConfig);
            services.Add(new ServiceDescriptor(typeof(IMailService), typeof(MailService), lifetime));
        }
    }
}
