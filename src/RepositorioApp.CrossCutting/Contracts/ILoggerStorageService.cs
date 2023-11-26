#nullable enable
using System;
namespace RepositorioApp.CrossCutting.Contracts
{
    public interface ILoggerStorageService
    {
        public void Info(string message);

        public void Warning(string message);

        public void Error(string message);

        public void Error(Exception ex, string? message = null);

        public void Critical(Exception ex, string? message = null);
    }
}
