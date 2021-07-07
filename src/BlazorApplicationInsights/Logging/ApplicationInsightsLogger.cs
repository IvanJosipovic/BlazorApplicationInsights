using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Globalization;

namespace BlazorApplicationInsights
{
    public class ApplicationInsightsLogger : ILogger
    {
        private static NullScope Scope { get; } = new NullScope();

        private readonly IApplicationInsights ApplicationInsights;

        public ApplicationInsightsLogger(IApplicationInsights applicationInsights)
        {
            ApplicationInsights = applicationInsights;
        }

        public IDisposable BeginScope<TState>(TState state)
        {
            return Scope;
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
            var properties = GetProperties(state);

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
                ApplicationInsights.TrackException(new Error() { Name = exception.GetType().Name, Message = exception.ToString() }, null, severityLevel, properties);
            }
            else
            {
                ApplicationInsights.TrackTrace(msg, severityLevel, properties);
            }
        }
        
        private Dictionary<string, object>? GetProperties<TState>(TState state)
        {
            var properties = state as IReadOnlyList<KeyValuePair<string, object>>;
            if (properties == null)
                return null;
            
            Dictionary<string, object> dict = new Dictionary<string, object>();
            foreach (KeyValuePair<string, object> item in properties)
                dict[item.Key] = Convert.ToString(item.Value, CultureInfo.InvariantCulture);

            return dict;
        }
    }

    internal class NullScope : IDisposable
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