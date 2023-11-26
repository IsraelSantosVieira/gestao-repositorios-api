using System;
namespace RepositorioApp.CrossCutting.Contracts
{
    public interface ILoggerAdapter<T>
    {
        void LogTrace(string message);
        void LogDebug(string message);
        void LogInformation(string message);
        void LogWarning(string message);
        void LogError(string message);
        void LogError(Exception exception);
        void LogCritical(string message);
        void LogCritical(Exception exception);
    }
}
