using Microsoft.Extensions.Logging;
using System;

namespace BlazorApplicationInsights
{
    internal class ApplicationInsightsLogger : ILogger
    {
        private static NullScope scope { get; } = new NullScope();

        private IApplicationInsights ApplicationInsights;

        public ApplicationInsightsLogger(IApplicationInsights applicationInsights)
        {
            ApplicationInsights = applicationInsights;
        }

        public IDisposable BeginScope<TState>(TState state)
        {
            return scope;
        }

        public bool IsEnabled(LogLevel logLevel)
        {
            return true;
        }

        public void Log<TState>(
            LogLevel logLevel,
            EventId eventId,
            TState state,
            Exception exception,
            Func<TState, Exception, string> formatter
        )
        {
            SeverityLevel severityLevel = SeverityLevel.Verbose;
            var msg = formatter(state, exception);

            switch (logLevel)
            {
                case LogLevel.Trace:
                case LogLevel.Debug:
                    severityLevel = SeverityLevel.Verbose;
                    break;
                case LogLevel.Information:
                    severityLevel = SeverityLevel.Information;
                    break;
                case LogLevel.Warning:
                    severityLevel = SeverityLevel.Warning;
                    break;
                case LogLevel.Error:
                    severityLevel = SeverityLevel.Error;
                    break;
                case LogLevel.Critical:
                case LogLevel.None:
                    severityLevel = SeverityLevel.Critical;
                    break;
            }

            if (exception != null)
            {
                ApplicationInsights.TrackException(new Error() { name = exception.GetType().Name, message = exception.ToString() }, severityLevel, null);
            }
            else
            {
                ApplicationInsights.TrackTrace(msg, severityLevel, null);
            }
        }
    }

    public class NullScope : IDisposable
    {
        public NullScope()
        {
        }

        /// <inheritdoc />
        public void Dispose()
        {
        }
    }
}