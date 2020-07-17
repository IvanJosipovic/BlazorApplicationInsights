using Microsoft.Extensions.Logging;

namespace BlazorApplicationInsights
{
    public class ApplicationInsightsLoggerProvider : ILoggerProvider
    {
        private readonly IApplicationInsights ApplicationInsights;
        private ILogger m_logger;
        private bool m_disposed = false;

        public ApplicationInsightsLoggerProvider(IApplicationInsights applicationInsights)
        {
            ApplicationInsights = applicationInsights;
        }

        public ILogger CreateLogger(string categoryName)
        {
            if (null == m_logger)
            {
                m_logger = new ApplicationInsightsLogger(ApplicationInsights);
            }

            return m_logger;
        }

        #region IDisposable Support

        protected virtual void Dispose(bool disposing)
        {
            if (!m_disposed)
            {
                if (disposing)
                {
                    m_logger = null;
                }

                m_disposed = true;
            }
        }

        public void Dispose()
        {
            Dispose(true);
        }

        #endregion
    }
}
