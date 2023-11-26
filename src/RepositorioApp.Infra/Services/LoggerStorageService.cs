#nullable enable
using System;
using Microsoft.Extensions.Logging;
using RepositorioApp.CrossCutting.Contracts;
using RepositorioApp.Extensions;
namespace RepositorioApp.Infra.Services
{
    public sealed class LoggerStorageService : ILoggerStorageService
    {
        public delegate void StoreLog(LogLevel level, string logger, string? message, Exception? exception = null);
        private readonly ILoggerAdapter<ILoggerStorageService> _loggerAdapter;
        private readonly string _loggerName;
        private readonly StoreLog _storeLog;

        public LoggerStorageService(string loggerName, StoreLog storeLog, ILoggerAdapter<ILoggerStorageService> loggerAdapter)
        {
            if (string.IsNullOrWhiteSpace(loggerName))
            {
                throw new ArgumentException("Logger name can't be empty or null", nameof(loggerName));
            }

            _loggerName = loggerName;
            _storeLog = storeLog ?? throw new ArgumentException("StoreLog can't be null", nameof(storeLog));
            _loggerAdapter = loggerAdapter;
        }

        public void Info(string message)
        {
            _storeLog(LogLevel.Information, _loggerName, message);
        }

        public void Warning(string message)
        {
            _storeLog(LogLevel.Warning, _loggerName, message);
        }

        public void Error(string message)
        {
            _storeLog(LogLevel.Error, _loggerName, message);
        }

        public void Error(Exception ex, string? message = null)
        {
            _loggerAdapter.LogError(ex);
            _storeLog(LogLevel.Error, _loggerName, message ?? ex.GetStackTraceMessage(), ex);
        }

        public void Critical(Exception ex, string? message = null)
        {
            _loggerAdapter.LogError(ex);
            _storeLog(LogLevel.Critical, _loggerName, message ?? ex.GetStackTraceMessage(), ex);
        }
    }
}
