#nullable enable
using System;
using Microsoft.Extensions.Logging;
using RepositorioApp.Extensions;
namespace RepositorioApp.CrossCutting.Utils
{
    public static class LoggerExtensions
    {
        private static readonly LogDefineOptions _logDefineOptions = new LogDefineOptions
        {
            SkipEnabledCheck = true
        };

        private static readonly Action<ILogger, string, Exception?> DefineLogTraceMessage = GetDefinition(LogLevel.Trace);

        private static readonly Action<ILogger, string, Exception?> DefineLogDebugMessage = GetDefinition(LogLevel.Debug);

        private static readonly Action<ILogger, string, Exception?> DefineLogInfoMessage = GetDefinition(LogLevel.Information);

        private static readonly Action<ILogger, string, Exception?> DefineLogWarningMessage = GetDefinition(LogLevel.Warning);

        private static readonly Action<ILogger, string, Exception?> DefineLogErrorMessage = GetDefinition(LogLevel.Error);

        private static readonly Action<ILogger, string, Exception?> DefineLogCritacalMessage = GetDefinition(LogLevel.Critical);

        private static Action<ILogger, string, Exception?> GetDefinition(LogLevel level)
        {
            return LoggerMessage.Define<string>(level, new EventId((int)level), "{message}", _logDefineOptions);
        }

        public static void LogMessage(this ILogger _logger, LogLevel level, string message, Exception? ex = null)
        {
            switch (level)
            {
                case LogLevel.Trace:
                    DefineLogTraceMessage(_logger, message, ex);
                    break;
                case LogLevel.Debug:
                    DefineLogDebugMessage(_logger, message, ex);
                    break;
                case LogLevel.Information:
                    DefineLogInfoMessage(_logger, message, ex);
                    break;
                case LogLevel.Warning:
                    DefineLogWarningMessage(_logger, message, ex);
                    break;
                case LogLevel.Error:
                    DefineLogErrorMessage(_logger, ex?.GetStackTraceMessage() ?? "Application Error", ex);
                    break;
                case LogLevel.Critical:
                    DefineLogCritacalMessage(_logger, ex?.GetStackTraceMessage() ?? "Application Error", ex);
                    break;
            }
        }
    }
}
