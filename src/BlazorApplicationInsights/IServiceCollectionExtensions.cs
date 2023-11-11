using System;
using BlazorApplicationInsights.Interfaces;
using BlazorApplicationInsights.Models;
using JetBrains.Annotations;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.DependencyInjection.Extensions;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;

namespace BlazorApplicationInsights;

[PublicAPI]
public static class IServiceCollectionExtensions
{
    // Nasty, but needed for unit testing.
    // ReSharper disable once MemberCanBePrivate.Global
    internal static bool PretendBrowserPlatform { get; set; }
    private static bool IsBrowserPlatform => PretendBrowserPlatform || OperatingSystem.IsBrowser();

    /// <summary>
    /// Adds the BlazorApplicationInsights services.
    /// </summary>
    /// <param name="services"></param>
    /// <param name="builder">Callback for configuring the service.</param>
    /// <param name="telemetryInitializer">Telemetry Initializer.</param>
    /// <param name="addWasmLogger">Adds the ILoggerProvider which ships all logs to Application Insights. This is disabled on Blazor Server.</param>
    /// <param name="loggingOptions">Callback for configuring the logging options. Blazor WASM only.</param>
    public static IServiceCollection AddBlazorApplicationInsights(this IServiceCollection services, Action<Config>? builder = null, Func<TelemetryItem, bool>? telemetryInitializer = null, bool addWasmLogger = true, Action<ApplicationInsightsLoggerOptions>? loggingOptions = null)
    {
        builder ??= delegate { };

        var initConfig = new ApplicationInsightsInitConfig();

        if (builder != null)
        {
            var config = new Config();
            builder(config);
            initConfig.Config = config;
        }

        if (telemetryInitializer != null)
        {
            initConfig.TelemetryInitializer = telemetryInitializer;
        }

        services.TryAddSingleton(initConfig);

        if (addWasmLogger && IsBrowserPlatform)
            services.TryAddEnumerable(ServiceDescriptor.Singleton<ILoggerProvider, ApplicationInsightsLoggerProvider>(x => CreateLoggerProvider(x, loggingOptions)));

        services.AddTransient<IApplicationInsights, ApplicationInsights>();

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

        public ApplicationInsightsLoggerOptions Get(string? name)
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