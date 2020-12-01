using System;
using System.Threading.Tasks;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;

namespace BlazorApplicationInsights
{
    public static class IServiceCollectionExtensions
    {
        /// <summary>
        /// Adds the BlazorApplicationInsights services.
        /// Blazor Server, set addILoggerProvider to false
        /// </summary>
        /// <param name="services"></param>
        /// <param name="addILoggerProvider"></param>
        /// <returns></returns>
        public static IServiceCollection AddBlazorApplicationInsights(this IServiceCollection services, Func<IApplicationInsights, Task> onInsightsInit, bool addILoggerProvider = true)
        {
            if (addILoggerProvider)
            {
                AddLoggerProvider(services);
            }

            return services.AddSingleton<IApplicationInsights>(factory => new ApplicationInsights(onInsightsInit));
        }

        /// <summary>
        /// Adds the BlazorApplicationInsights services.
        /// Blazor Server, set addILoggerProvider to false
        /// </summary>
        /// <param name="services"></param>
        /// <param name="addILoggerProvider"></param>
        /// <returns></returns>
        public static IServiceCollection AddBlazorApplicationInsights(this IServiceCollection services, bool addILoggerProvider = true)
        {
            if (addILoggerProvider)
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