using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;

namespace BlazorApplicationInsights
{
    public static class IServiceCollectionExtensions
    {
        public static IServiceCollection AddBlazorApplicationInsights(this IServiceCollection services)
        {
            services.AddSingleton<ILoggerProvider, ApplicationInsightsLoggerProvider>();
            return services.AddSingleton<IApplicationInsights, ApplicationInsights>();
        }
    }
}