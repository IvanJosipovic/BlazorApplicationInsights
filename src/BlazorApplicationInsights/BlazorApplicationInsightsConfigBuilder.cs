using System;
using System.Threading.Tasks;
using JetBrains.Annotations;
using Microsoft.Extensions.Logging;

namespace BlazorApplicationInsights
{
    /// <summary>
    /// Builder for configuring on AddBlazorApplicationInsights
    /// </summary>
    [PublicAPI]
    public class BlazorAppInsightsConfigBuilder
    {
        internal bool ShouldAddLogger { get; private set; }
        internal bool ShouldSetEnableAutoRouteTracking { get; private set; } = true;
        internal  Action<ApplicationInsightsLoggerOptions>? CallbackConfigureLoggerOptions { get; private set; }
        internal Func<IApplicationInsights, Task> CallbackInitializingAppInsights { get; private set; }

        /// <summary>
        /// Add a provider for creating <see cref="ILogger"/> instances
        /// </summary>
        /// <remarks>Note that this does not enable logging in and off itself - calling AddLogging() on the service provider is still required</remarks>
        /// <param name="addLogger"><see langword="true"/> to add the logger; otherwise <see langword="false"/></param>
        /// <param name="configure">Callback for configuring the logger settings</param>
        /// <returns></returns>
        public BlazorAppInsightsConfigBuilder AddLogger(bool addLogger, Action<ApplicationInsightsLoggerOptions>? configure = null)
        {
            ShouldAddLogger = true;
            CallbackConfigureLoggerOptions = configure;
            return this;
        }

        /// <summary>
        /// Add a provider for creating <see cref="ILogger"/> instances
        /// </summary>
        /// <remarks>Note that this does not enable logging in and off itself - calling AddLogging() on the service provider is still required</remarks>
        /// <param name="configure">Callback for configuring the logger settings</param>
        /// <returns></returns>
        public BlazorAppInsightsConfigBuilder AddLogger(Action<ApplicationInsightsLoggerOptions>? configure = null) => AddLogger(true, configure);

        public BlazorAppInsightsConfigBuilder SetEnableAutoRouteTracking(bool state)
        {
            ShouldSetEnableAutoRouteTracking = state;
            return this;
        }

        public BlazorAppInsightsConfigBuilder WithOnInitCallback(Func<IApplicationInsights, Task> callback)
        {
            CallbackInitializingAppInsights = callback;
            return this;
        }
    }
}
