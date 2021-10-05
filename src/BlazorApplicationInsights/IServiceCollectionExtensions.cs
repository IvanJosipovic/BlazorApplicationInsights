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
        /// For Blazor Server, addILoggerProvider is automatically set to false
        /// </summary>
        /// <param name="services"></param>
        /// <param name="onInsightsInit"></param>
        /// <param name="addILoggerProvider"></param>
        /// <returns>IServiceCollection</returns>
        public static IServiceCollection AddBlazorApplicationInsights(this IServiceCollection services, Func<IApplicationInsights, Task> onInsightsInit, bool addILoggerProvider = true)
        {
            if (addILoggerProvider && RuntimeInformation.IsOSPlatform(OSPlatform.Create("BROWSER")))
            {
                AddLoggerProvider(services);
            }

            return services.AddSingleton<IApplicationInsights>(_ => new ApplicationInsights(onInsightsInit));
        }

        /// <summary>
        /// Adds the BlazorApplicationInsights services.
        /// For Blazor Server, addILoggerProvider is automatically set to false
        /// </summary>
        /// <param name="services"></param>
        /// <param name="addILoggerProvider"></param>
        /// <returns>IServiceCollection</returns>
        public static IServiceCollection AddBlazorApplicationInsights(this IServiceCollection services, bool addILoggerProvider = true)
        {
            if (addILoggerProvider && RuntimeInformation.IsOSPlatform(OSPlatform.Create("BROWSER")))
            {
                AddLoggerProvider(services);
            }

            return services.AddSingleton<IApplicationInsights, ApplicationInsights>();
        }

        private static void AddLoggerProvider(IServiceCollection services)
        {
            services.AddSingleton<ILoggerProvider, ApplicationInsightsLoggerProvider>();
        }
    }
}