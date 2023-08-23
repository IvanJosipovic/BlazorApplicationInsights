using System;
using System.Linq;
using System.Runtime.InteropServices;
using System.Threading.Tasks;
using JetBrains.Annotations;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.DependencyInjection.Extensions;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;

namespace BlazorApplicationInsights
{
    [PublicAPI]
    public static class IServiceCollectionExtensions
    {
        // Nasty, but needed for unit testing.
        // ReSharper disable once MemberCanBePrivate.Global
        internal static bool PretendBrowserPlatform { get; set; }
        private static bool IsBrowserPlatform => PretendBrowserPlatform || RuntimeInformation.IsOSPlatform(OSPlatform.Create("BROWSER"));

        /// <summary>
        /// Adds the BlazorApplicationInsights services.
        /// </summary>
        /// <param name="services"></param>
        /// <param name="onInsightsInit"></param>
        /// <param name="addILoggerProvider">Adds the ILogerProver which ships all logs to Application Insights. This is disabled on Blazor Server.</param>
        /// <param name="enableAutoRouteTracking">Enables automatic Track Page View on Route changes</param>
        /// <returns>IServiceCollection</returns>
        public static IServiceCollection AddBlazorApplicationInsights(this IServiceCollection services, Func<IApplicationInsights, Task> onInsightsInit, bool addILoggerProvider = true, bool enableAutoRouteTracking = true)
            => AddBlazorApplicationInsights(services, config => config.AddLogger(addILoggerProvider).SetEnableAutoRouteTracking(enableAutoRouteTracking).WithOnInitCallback(onInsightsInit));

        /// <summary>
        /// Adds the BlazorApplicationInsights services.
        /// </summary>
        /// <param name="services"></param>
        /// <param name="addILoggerProvider">Adds the ILogerProver which ships all logs to Application Insights. This is disabled on Blazor Server.</param>
        /// <param name="enableAutoRouteTracking">Enables automatic Track Page View on Route changes</param>
        /// <returns>IServiceCollection</returns>
        public static IServiceCollection AddBlazorApplicationInsights(this IServiceCollection services, bool addILoggerProvider = true, bool enableAutoRouteTracking = true)
            => AddBlazorApplicationInsights(services, config => config.AddLogger(addILoggerProvider).SetEnableAutoRouteTracking(enableAutoRouteTracking));


        /// <summary>
        /// Adds the BlazorApplicationInsights services.
        /// </summary>
        /// <param name="services"></param>
        /// <param name="builder">Callback for configuring the service</param>
        /// <returns></returns>
        public static IServiceCollection AddBlazorApplicationInsights(this IServiceCollection services, Action<BlazorAppInsightsConfigBuilder> builder)
        {
            var configBuilder = new BlazorAppInsightsConfigBuilder();
            builder(configBuilder);

            if (configBuilder.ShouldAddLogger && IsBrowserPlatform)
                services.TryAddEnumerable(ServiceDescriptor.Singleton<ILoggerProvider, ApplicationInsightsLoggerProvider>(x => CreateLoggerProvider(x, configBuilder.CallbackConfigureLoggerOptions)));

            services.TryAddSingleton<IApplicationInsights>(_ => new ApplicationInsights(configBuilder.CallbackInitializingAppInsights) {EnableAutoRouteTracking = configBuilder.ShouldSetEnableAutoRouteTracking });
            return services;
        }

        private static ApplicationInsightsLoggerProvider CreateLoggerProvider(IServiceProvider services, Action<ApplicationInsightsLoggerOptions>? configure)
        {
            configure ??= delegate { };

            var options = new ApplicationInsightsLoggerOptions();
            configure(options);

            // Sure, this is a little insane, but I had already gone with IOptions
            // before ripping out Microsoft.Extensions.Logging.Configuration.
            // Rather than redoing the plumbing, let's just keep it and
            // if we want to add Logging.Configuration later it should be easy.
            var optionsMonitor = new DummyOptionsMonitor(options);
            var appInsights = services.GetRequiredService<IApplicationInsights>();

            return new ApplicationInsightsLoggerProvider(appInsights, optionsMonitor);
        }

        private class DummyOptionsMonitor : IOptionsMonitor<ApplicationInsightsLoggerOptions>
        {
            public DummyOptionsMonitor(ApplicationInsightsLoggerOptions currentValue)
            {
                CurrentValue = currentValue;
            }

            public ApplicationInsightsLoggerOptions Get(string name)
            {
                if (name != string.Empty)
                    return null;

                return CurrentValue;
            }

            public IDisposable OnChange(Action<ApplicationInsightsLoggerOptions, string> listener)
                => NoOpDisposable.Instance;

            public ApplicationInsightsLoggerOptions CurrentValue { get; set; }
        }
    }
}