using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;

namespace BlazorApplicationInsights
{
    public static class IServiceCollectionExtensions
    {
        /// <summary>
        /// Adds the BlazorApplicationInsigts services.
        /// </summary>
        /// <param name="services"></param>
        /// <param name="addILoggerProvider"></param>
        /// <returns></returns>
        public static IServiceCollection AddBlazorApplicationInsights(this IServiceCollection services, bool addILoggerProvider = true)
        {
            if (addILoggerProvider)
            {
                services.AddSingleton<ILoggerProvider, ApplicationInsightsLoggerProvider>();
            }
            
            return services.AddSingleton<IApplicationInsights, ApplicationInsights>();
        }
    }
}