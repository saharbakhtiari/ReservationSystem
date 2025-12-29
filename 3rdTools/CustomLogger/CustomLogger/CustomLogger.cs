using Microsoft.Extensions.Logging;
using System;
using System.Threading.Tasks;
using LogLevel = Microsoft.Extensions.Logging.LogLevel;

namespace CustomLoggers
{
    public class CustomLogger<TCategoryName> : ICustomLogger<TCategoryName>
    {
        private readonly ILogger<TCategoryName> _logger;

        public CustomLogger(ILogger<TCategoryName> logger)
        {
            _logger = logger;
        }
        public Task Log(LogLevel logLevel, Exception exception, string message, params object[] args)
        {
            return TaskRunner(() => _logger.Log(logLevel, exception, message, args));
        }

        public Task Log(LogLevel logLevel, EventId eventId, string message, params object[] args)
        {
            return TaskRunner(() => _logger.Log(logLevel, eventId, message, args));
        }

        public Task Log(LogLevel logLevel, string message, params object[] args)
        {
            return TaskRunner(() => _logger.Log(logLevel, message, args));
        }

        public Task Log(LogLevel logLevel, EventId eventId, Exception exception, string message, params object[] args)
        {
            return TaskRunner(() => _logger.Log(logLevel, eventId, exception, message, args));
        }

        public Task LogCritical(string message, params object[] args)
        {
            return TaskRunner(() => _logger.LogCritical(message, args));
        }

        public Task LogCritical(Exception exception, string message, params object[] args)
        {
            return TaskRunner(() => _logger.LogCritical(exception, message, args));
        }

        public Task LogCritical(EventId eventId, string message, params object[] args)
        {
            return TaskRunner(() => _logger.LogCritical(eventId, message, args));
        }

        public Task LogCritical(EventId eventId, Exception exception, string message, params object[] args)
        {
            return TaskRunner(() => _logger.LogCritical(eventId, exception, message, args));
        }

        public Task LogDebug(EventId eventId, Exception exception, string message, params object[] args)
        {
            return TaskRunner(() => _logger.LogDebug(eventId, exception, message, args));
        }

        public Task LogDebug(EventId eventId, string message, params object[] args)
        {
            return TaskRunner(() => _logger.LogDebug(eventId, message, args));
        }

        public Task LogDebug(Exception exception, string message, params object[] args)
        {
            return TaskRunner(() => _logger.LogDebug(exception, message, args));
        }

        public Task LogDebug(string message, params object[] args)
        {
            return TaskRunner(() => _logger.LogDebug(message, args));
        }

        public Task LogError(string message, params object[] args)
        {
            return TaskRunner(() => _logger.LogError(message, args));
        }

        public Task LogError(Exception exception, string message, params object[] args)
        {
            return TaskRunner(() => _logger.LogError(exception, message, args));
        }

        public Task LogError(EventId eventId, Exception exception, string message, params object[] args)
        {
            return TaskRunner(() => _logger.LogError(eventId, exception, message, args));
        }

        public Task LogError(EventId eventId, string message, params object[] args)
        {
            return TaskRunner(() => _logger.LogError(eventId, message, args));
        }

        public Task LogInformation(EventId eventId, string message, params object[] args)
        {
            return TaskRunner(() => _logger.LogInformation(eventId, message, args));
        }

        public Task LogInformation(Exception exception, string message, params object[] args)
        {
            return TaskRunner(() => _logger.LogInformation(exception, message, args));
        }

        public Task LogInformation(EventId eventId, Exception exception, string message, params object[] args)
        {
            return TaskRunner(() => _logger.LogInformation(eventId, exception, message, args));
        }

        public Task LogInformation(string message, params object[] args)
        {
            return TaskRunner(() => _logger.LogInformation(message, args));
        }

        public Task LogTrace(string message, params object[] args)
        {
            return TaskRunner(() => _logger.LogTrace(message, args));
        }

        public Task LogTrace(Exception exception, string message, params object[] args)
        {
            return TaskRunner(() => _logger.LogTrace(exception, message, args));
        }

        public Task LogTrace(EventId eventId, string message, params object[] args)
        {
            return TaskRunner(() => _logger.LogTrace(eventId, message, args));
        }

        public Task LogTrace(EventId eventId, Exception exception, string message, params object[] args)
        {
            return TaskRunner(() => _logger.LogTrace(eventId, exception, message, args));
        }

        public Task LogWarning(EventId eventId, string message, params object[] args)
        {
            return TaskRunner(() => _logger.LogWarning(eventId, message, args));
        }

        public Task LogWarning(EventId eventId, Exception exception, string message, params object[] args)
        {
            return TaskRunner(() => _logger.LogWarning(eventId, exception, message, args));
        }

        public Task LogWarning(string message, params object[] args)
        {
            return TaskRunner(() => _logger.LogWarning(message, args));
        }

        public Task LogWarning(Exception exception, string message, params object[] args)
        {
            return TaskRunner(() => _logger.LogWarning(exception, message, args));
        }

        private Task TaskRunner(Action action)
        {
            return Task.Run(() =>
            {
                action();
            });
        }
    }

}
