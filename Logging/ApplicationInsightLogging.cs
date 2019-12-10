using Microsoft.Extensions.Logging;
using System;

namespace ProfileMicroservice.Logging
{
    public class ApplicationInsightLogging : ILogger
    {
        private readonly ILogger<ApplicationInsightLogging> _logger;
        public ApplicationInsightLogging(ILogger<ApplicationInsightLogging> logger)
        {
            _logger = logger;
        }
        public void LogCritical(EventId eventId, Exception exception, string message, params object[] args)
        {
            _logger.LogCritical(eventId, exception, message, args);
        }

        public void LogCritical(string message, params object[] args)
        {
            _logger.LogCritical(message, args);
        }

        public void LogDebug(EventId eventId, Exception exception, string message, params object[] args)
        {
            _logger.LogDebug(eventId, exception, message, args);
        }

        public void LogDebug(string message, params object[] args)
        {
            _logger.LogDebug(message, args);
        }

        public void LogError(EventId eventId, Exception exception, string message, params object[] args)
        {
            _logger.LogError(eventId, exception, message, args);
        }

        public void LogError(string message, params object[] args)
        {
            _logger.LogError(message, args);
        }
        public void LogInformation(EventId eventId, Exception exception, string message, params object[] args)
        {
            _logger.LogInformation(eventId, exception, message, args);
        }

        public void LogInformation(string message, params object[] args)
        {
            _logger.LogInformation(message, args);
        }

        public void LogTrace(EventId eventId, Exception exception, string message, params object[] args)
        {
            _logger.LogInformation(eventId, exception, message, args);
        }

        public void LogWarning(EventId eventId, Exception exception, string message, params object[] args)
        {
            _logger.LogWarning(eventId, exception, message, args);
        }

        public void LogWarning(string message, params object[] args)
        {
            _logger.LogWarning(message, args);
        }
    }
}
