using Application.Interfaces;
using Microsoft.Extensions.Logging;

namespace Infrastructure
{
    public class LoggingManager<T>(ILogger<T> logger)
        : ILoggingManager<T>
    {
        private readonly ILogger<T> _logger = logger;

        public void LogInformation(string message, params object[] args)
            => _logger.LogInformation(message, args);

        public void LogError(Exception exception, string message, params object[] args)
            => _logger.LogError(exception, message, args);

        public void LogWarning(string message, params object[] args)
            => _logger.LogWarning(message, args);

        public void LogDebug(string message, params object[] args)
            => _logger.LogDebug(message, args);

        public void LogException(Exception ex)
            => _logger.LogError(ex, null);
    }
}
