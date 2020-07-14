using Microsoft.Extensions.DependencyInjection;

namespace BlazorApplicationInsights
{
    public static class IServiceCollectionExtensions
    {
        public static IServiceCollection AddBlazorApplicationInsights(this IServiceCollection services)
        {
            return services.AddSingleton<IApplicationInsights, ApplicationInsights>();
        }
    }
}