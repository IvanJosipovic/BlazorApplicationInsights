using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using BlazorApplicationInsights.Interfaces;
using JetBrains.Annotations;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;

// ReSharper disable once CheckNamespace
namespace BlazorApplicationInsights
{
    /// <summary>LoggerProvider implementation for logging to Application Insights in Blazor Client-Side (WASM) applications</summary>
    [PublicAPI]
    public class ApplicationInsightsLoggerProvider : ILoggerProvider, ISupportExternalScope
    {
        private readonly IApplicationInsights _applicationInsights;
        private readonly ConcurrentDictionary<string, ApplicationInsightsLogger> _loggers = new ConcurrentDictionary<string, ApplicationInsightsLogger>();
        private readonly IDisposable _optionsReloadToken;
        private Action<Dictionary<string, object?>> _enrichmentCallback = delegate { };
        private IExternalScopeProvider? _scopeProvider;
        private ApplicationInsightsLoggerOptions _options = new ApplicationInsightsLoggerOptions();
        private bool _isDisposed;

        /// <summary>Initializes a new instance of the <see cref="ApplicationInsightsLoggerProvider"/> class</summary>
        /// <param name="applicationInsights">Instance to use for transmitting logging messages</param>
        public ApplicationInsightsLoggerProvider(IApplicationInsights applicationInsights)
        {
            _applicationInsights = applicationInsights;
            _optionsReloadToken = NoOpDisposable.Instance;
        }

        /// <summary>Initializes a new instance of the <see cref="ApplicationInsightsLoggerProvider"/> class</summary>
        /// <param name="applicationInsights">Instance to use for transmitting logging messages</param>
        /// <param name="options">Logger options</param>
        [ActivatorUtilitiesConstructor]
        public ApplicationInsightsLoggerProvider(IApplicationInsights applicationInsights, IOptionsMonitor<ApplicationInsightsLoggerOptions> options)
        {
            _applicationInsights = applicationInsights;
            _optionsReloadToken = options.OnChange(ReloadOptions);
            ReloadOptions(options.CurrentValue);
        }

        /// <inheritdoc />
        public ILogger CreateLogger(string categoryName) =>
            _loggers.GetOrAdd(categoryName, CreateLoggerInstance);

        /// <inheritdoc />
        public void SetScopeProvider(IExternalScopeProvider scopeProvider)
        {
            _scopeProvider = scopeProvider;

            foreach (var logger in _loggers.Values)
                logger.ScopeProvider = scopeProvider;
        }

        /// <inheritdoc />
        public void Dispose()
        {
            if (_isDisposed)
                return;

            _isDisposed = true;
            _optionsReloadToken.Dispose();
        }

        private ApplicationInsightsLogger CreateLoggerInstance(string categoryName)
        {
            return new ApplicationInsightsLogger(categoryName, _applicationInsights)
            {
                ScopeProvider = GetScopeProvider(),
                IncludeScopes = _options.IncludeScopes,
                IncludeCategoryName = _options.IncludeCategoryName,

#pragma warning disable 618
                EnrichmentCallback = _enrichmentCallback
#pragma warning restore 618
            };
        }

        private IExternalScopeProvider GetScopeProvider()
        {
            _scopeProvider ??= new LoggerExternalScopeProvider();
            return _scopeProvider;
        }

        private void ReloadOptions(ApplicationInsightsLoggerOptions options)
        {
            _options = options;

#pragma warning disable 618
            _enrichmentCallback = options.EnrichCallback ?? delegate { };
#pragma warning restore 618

            var scopeProvider = GetScopeProvider();
            foreach (var logger in _loggers.Values)
            {
                logger.ScopeProvider = scopeProvider;
                logger.IncludeCategoryName = options.IncludeCategoryName;
                logger.IncludeScopes = options.IncludeScopes;

#pragma warning disable 618
                logger.EnrichmentCallback = _enrichmentCallback;
#pragma warning restore 618
            }
        }
    }
}
