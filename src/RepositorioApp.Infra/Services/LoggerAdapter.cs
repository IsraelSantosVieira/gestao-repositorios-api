using System;
using Microsoft.Extensions.Logging;
using RepositorioApp.CrossCutting.Contracts;
using RepositorioApp.CrossCutting.Utils;
using RepositorioApp.Extensions;
namespace RepositorioApp.Infra.Services
{
    public sealed class LoggerAdapter<T> : ILoggerAdapter<T>
    {
        private readonly ILogger<T> _logger;

        public LoggerAdapter(ILogger<T> logger)
        {
            _logger = logger;
        }

        public void LogTrace(string message)
        {
            _logger.LogMessage(LogLevel.Trace, message);
        }
        public void LogDebug(string message)
        {
            _logger.LogMessage(LogLevel.Debug, message);
        }
        public void LogInformation(string message)
        {
            _logger.LogMessage(LogLevel.Information, message);
        }
        public void LogWarning(string message)
        {
            _logger.LogMessage(LogLevel.Warning, message);
        }
        public void LogError(string message)
        {
            _logger.LogMessage(LogLevel.Error, message);
        }
        public void LogError(Exception exception)
        {
            _logger.LogMessage(LogLevel.Error, exception.GetStackTraceMessage(), exception);
        }
        public void LogCritical(string message)
        {
            _logger.LogMessage(LogLevel.Critical, message);
        }
        public void LogCritical(Exception exception)
        {
            _logger.LogMessage(LogLevel.Critical, exception.GetStackTraceMessage(), exception);
        }
    }
}
