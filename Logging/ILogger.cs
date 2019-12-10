using Microsoft.Extensions.Logging;
using System;

namespace ProfileMicroservice.Logging
{
    public interface ILogger
    {
        void LogDebug(EventId eventId, Exception exception, string message, params object[] args);
        void LogDebug(string message, params object[] args);

        void LogTrace(EventId eventId, Exception exception, string message, params object[] args);

        void LogInformation(EventId eventId, Exception exception, string message, params object[] args);
        void LogInformation(string message, params object[] args);

        void LogWarning(EventId eventId, Exception exception, string message, params object[] args);
        void LogWarning(string message, params object[] args);


        void LogError(EventId eventId, Exception exception, string message, params object[] args);
        void LogError(string message, params object[] args);


        void LogCritical(EventId eventId, Exception exception, string message, params object[] args);
        void LogCritical(string message, params object[] args);
    }
}
