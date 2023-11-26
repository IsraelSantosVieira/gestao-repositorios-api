using System;
using System.Linq;
using FluentValidation;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using RepositorioApp.CrossCutting.Contracts;
using RepositorioApp.Data;
using RepositorioApp.Data.Repositories;
using RepositorioApp.Domain.AppServices.Users;
using RepositorioApp.Domain.AppServices.Users.Validators;
using RepositorioApp.Domain.Contracts.Infra;
using RepositorioApp.Infra.Security;
using RepositorioApp.Infra.Services;
using RepositorioApp.Infra.Services.Jobs;
using RepositorioApp.Mail;
using RepositorioApp.Persistence.Ef;
using RepositorioApp.Utilities.Notifications;
using RepositorioApp.Utilities.Persistence;
namespace RepositorioApp.Api._Config.Startup
{
    public static class IoCServicesConfig
    {
        public static IServiceCollection AppAddIoCServices(this IServiceCollection services, IConfiguration config, IHostEnvironment env)
        {
            //infra

            services.AddRazorTemplating();

            services.AddSingleton<IMailService, MailService>();

            services.AddScoped<IViewRenderWrapper, ViewRenderWrapper>();

            services.AddSingleton<IHttpContextAccessor, HttpContextAccessor>();

            services.AddScoped<ISessionProvider, SessionProvider>();

            services.AddScoped<IJobService, JobsService>();

            services.AddScoped<IStorageService, StorageService>();

            //events
            services.AddScoped<IDomainNotification, DomainNotification>();

            // unit of work
            services.AddScoped<IUnitOfWork, UnitOfWork<DataContext>>();

            //validators
            services.AddValidatorsFromAssembly(typeof(AuthenticateUserValidator).Assembly);

            //services
            typeof(AuthenticateUserAppService).Assembly.GetTypes()
                .Where(x => x.FullName != null &&
                            x.FullName.Contains("AppServices") &&
                            x.FullName.Contains("Domain") &&
                            x.GetInterfaces().Any() &&
                            x.IsClass)
                .ToList().ForEach(x =>
                {
                    var @interface = x.GetInterfaces().FirstOrDefault(s => s.Name.Contains(x.Name));

                    if (@interface == null) return;

                    services.AddScoped(@interface, x);
                });


            //repositories
            typeof(UserRepository).Assembly.GetTypes()
                .Where(x => x.FullName != null &&
                            x.FullName.Contains("Repositories") &&
                            x.GetInterfaces().Any() &&
                            x.IsClass &&
                            x != typeof(Repository<,>))
                .ToList().ForEach(x =>
                {
                    var @interface = x.GetInterfaces()
                        .FirstOrDefault(s => s.Name.Contains(x.Name));

                    if (@interface == null) return;

                    services.AddScoped(@interface, x);
                });

            return services;
        }

        public static IConfiguration ConfigureEnvVariables<T>(this IConfiguration config,
            IServiceCollection services)
            where T : class
        {
            var instance = (T)Activator.CreateInstance(typeof(T));
            config.Bind(instance?.GetType().Name, instance);
            if (instance != null) services.AddSingleton(instance);
            return config;
        }
    }
}
