using System;
using System.Data;
using Dapper;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Npgsql;
using RepositorioApp.CrossCutting.Config;
using RepositorioApp.CrossCutting.Contracts;
using RepositorioApp.Domain.Entities;
using RepositorioApp.Extensions;
using RepositorioApp.Infra.Services;
namespace RepositorioApp.Api._Config.Startup
{
    public static class LoggerConfig
    {
        public static IServiceCollection AddLogger(this IServiceCollection services, IConfiguration config)
        {
            services.AddSingleton(typeof(ILoggerAdapter<>), typeof(LoggerAdapter<>));

            services.AddSingleton<ILoggerStorageService>(sp =>
                new LoggerStorageService(
                    config.GetSection($"{nameof(AppConfig)}:{nameof(AppConfig.Logger)}").Value,
                    StoreLog(config.GetConnectionString("Default")),
                    sp.GetRequiredService<ILoggerAdapter<ILoggerStorageService>>()));

            return services;
        }

        private static LoggerStorageService.StoreLog StoreLog(string connectionString)
        {
            return (level, logger, message, exception) =>
            {
                using var sqlConnection = new NpgsqlConnection(connectionString);
                try
                {
                    var log = new Log
                    {
                        Id = Guid.NewGuid(),
                        Exception = exception?.GetStackTraceMessage(),
                        Message = message,
                        Level = level.ToString(),
                        Logger = logger,
                        OccurredAt = DateTime.UtcNow
                    };

                    var sql = @"
                    set schema 'public';
                    insert into logs (id, occurred_at, level, logger, message, exception)
                    values (@Id, @OccurredAt, @Level, @Logger, @Message, @Exception)";

                    sqlConnection.Execute(sql,
                        log,
                        commandType: CommandType.Text);
                }
                finally
                {
                    sqlConnection.Close();
                }
            };
        }
    }
}
