using System;
using System.Runtime.InteropServices;
using System.Threading.Tasks;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;

namespace BlazorApplicationInsights
{
    public static class IServiceCollectionExtensions
    {
        /// <summary>
        /// Adds the BlazorApplicationInsights services.
        /// </summary>
        /// <param name="services"></param>
        /// <param name="onInsightsInit"></param>
        /// <param name="addILoggerProvider">Adds the ILogerProver which ships all logs to Application Insights. This is disabled on Blazor Server.</param>
        /// <param name="enableAutoRouteTracking">Enables automatic Track Page View on Route changes</param>
        /// <returns>IServiceCollection</returns>
        public static IServiceCollection AddBlazorApplicationInsights(this IServiceCollection services, Func<IApplicationInsights, Task> onInsightsInit, bool addILoggerProvider = true, bool enableAutoRouteTracking = true)
        {
            if (addILoggerProvider && RuntimeInformation.IsOSPlatform(OSPlatform.Create("BROWSER")))
            {
                AddLoggerProvider(services);
            }

            return services.AddSingleton<IApplicationInsights>(_ => new ApplicationInsights(onInsightsInit) { EnableAutoRouteTracking = enableAutoRouteTracking });
        }

        /// <summary>
        /// Adds the BlazorApplicationInsights services.
        /// </summary>
        /// <param name="services"></param>
        /// <param name="addILoggerProvider">Adds the ILogerProver which ships all logs to Application Insights. This is disabled on Blazor Server.</param>
        /// <param name="enableAutoRouteTracking">Enables automatic Track Page View on Route changes</param>
        /// <returns>IServiceCollection</returns>
        public static IServiceCollection AddBlazorApplicationInsights(this IServiceCollection services, bool addILoggerProvider = true, bool enableAutoRouteTracking = true)
        {
            if (addILoggerProvider && RuntimeInformation.IsOSPlatform(OSPlatform.Create("BROWSER")))
            {
                AddLoggerProvider(services);
            }

            return services.AddSingleton<IApplicationInsights>(_ => new ApplicationInsights() { EnableAutoRouteTracking = enableAutoRouteTracking });
        }

        private static void AddLoggerProvider(IServiceCollection services)
        {
            services.AddSingleton<ILoggerProvider, ApplicationInsightsLoggerProvider>();
        }
    }
}