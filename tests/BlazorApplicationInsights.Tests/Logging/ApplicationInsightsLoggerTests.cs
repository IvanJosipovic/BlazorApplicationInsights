using System;
using System.Collections.Generic;
using BlazorApplicationInsights.Interfaces;
using BlazorApplicationInsights.Models;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Microsoft.JSInterop;
using Moq;
using Xunit;

#nullable enable

namespace BlazorApplicationInsights.Tests.Logging
{
    public class ApplicationInsightsLoggerTests
    {
        private const string MessageSimple = "This is a simple test message";
        private readonly Mock<IApplicationInsights> _appInsightsMock = new();
        private readonly Mock<IJSRuntime> _jsRuntimeMock = new();

        public ApplicationInsightsLoggerTests()
        {
            IServiceCollectionExtensions.PretendBrowserPlatform = true;
        }

        [Fact]
        public void Should_work_without_category_name()
        {
            var receivedProperties = new Dictionary<string, object?>();
            SetupTrackTrace((x) => receivedProperties = x.Properties);

            var logger = new ApplicationInsightsLogger(_appInsightsMock.Object);
            logger.LogInformation(MessageSimple);

            Assert.Single(receivedProperties);
            Assert.Collection(receivedProperties, item => Assert.Equal(KeyValuePair.Create<string, object?>("OriginalFormat", MessageSimple), item));
        }

        [Fact]
        public void Should_write_using_track_exceptions_when_an_exception_is_attached()
        {
            var services = CreateServiceProvider();

            var receivedProperties = new Dictionary<string, object?>();
            SetupTrackException((x) => receivedProperties = x.Properties);

            var ex = new InvalidOperationException("Invalid state");
            var logger = services.GetRequiredService<ILogger<ApplicationInsightsLoggerTests>>();
            logger.LogError(ex, MessageSimple);

            _appInsightsMock.Verify(x => x.TrackException(It.Is<ExceptionTelemetry>(trace => trace.Exception.Message == ex.Message && trace.Exception.Name == nameof(InvalidOperationException) && trace.Id == "0" && trace.SeverityLevel == SeverityLevel.Error)),
                Times.Once);

            _appInsightsMock.VerifyNoOtherCalls();

            Assert.Single(receivedProperties);
            Assert.Collection(receivedProperties,
                item => Assert.Equal(KeyValuePair.Create<string, object?>("OriginalFormat", MessageSimple), item)
            );
        }

        [Fact]
        public void Should_build_scope_path_string()
        {
            var services = CreateServiceProvider(x =>
            {
                x.IncludeScopes = true;
                x.IncludeCategoryName = false;
            });

            var receivedProperties = new Dictionary<string, object?>();
            SetupTrackTrace((x) => receivedProperties = x.Properties);

            var logger = services.GetRequiredService<ILogger<ApplicationInsightsLoggerTests>>();

            using var outerScope = logger.BeginScope("Outer Scope");
            using var middleScope = logger.BeginScope("Middle Scope");
            using var innerScope = logger.BeginScope("Inner Scope");

            logger.LogInformation(MessageSimple);

            _appInsightsMock.Verify(x => x.TrackTrace(It.Is<TraceTelemetry>(trace => trace.Message == MessageSimple && trace.SeverityLevel == SeverityLevel.Information)),
            Times.Once);

            _appInsightsMock.VerifyNoOtherCalls();

            Assert.Equal(2, receivedProperties.Count);
            Assert.Collection(receivedProperties,
                item => Assert.Equal(KeyValuePair.Create<string, object?>("Scope", " => Outer Scope => Middle Scope => Inner Scope"), item),
                item => Assert.Equal(KeyValuePair.Create<string, object?>("OriginalFormat", MessageSimple), item));
        }

        [Fact]
        public void Should_include_CategoryName_when_enabled()
        {
            var services = CreateServiceProvider(x =>
            {
                x.IncludeScopes = false;
                x.IncludeCategoryName = true;
            });

            var receivedProperties = new Dictionary<string, object?>();
            SetupTrackTrace((x) => receivedProperties = x.Properties);

            var logger = services.GetRequiredService<ILogger<ApplicationInsightsLoggerTests>>();
            logger.LogInformation(MessageSimple);

            _appInsightsMock.Verify(x => x.TrackTrace(It.Is<TraceTelemetry>(trace => trace.Message == MessageSimple && trace.SeverityLevel == SeverityLevel.Information)),
                Times.Once);
            _appInsightsMock.VerifyNoOtherCalls();

            Assert.Equal(2, receivedProperties.Count);
            Assert.Collection(receivedProperties,
                item => Assert.Equal(KeyValuePair.Create<string, object?>("CategoryName", typeof(ApplicationInsightsLoggerTests).FullName), item),
                item => Assert.Equal(KeyValuePair.Create<string, object?>("OriginalFormat", MessageSimple), item));
        }

        [Fact]
        public void Should_allow_custom_enrichment()
        {
            const string enrichedKey = "EnrichedKey";
            const string enrichedData = "Enriched Data!";
            var services = CreateServiceProvider(x =>
            {
                x.IncludeScopes = true;
                x.IncludeCategoryName = false;
#pragma warning disable 618
                x.EnrichCallback = dict => dict[enrichedKey] = enrichedData;
#pragma warning restore 618
            });

            var receivedProperties = new Dictionary<string, object?>();
            SetupTrackTrace((x) => receivedProperties = x.Properties);

            var logger = services.GetRequiredService<ILogger<ApplicationInsightsLoggerTests>>();
            logger.LogInformation(MessageSimple);

            _appInsightsMock.Verify(x => x.TrackTrace(It.Is<TraceTelemetry>(trace => trace.Message == MessageSimple && trace.SeverityLevel == SeverityLevel.Information)),
                Times.Once);
            _appInsightsMock.VerifyNoOtherCalls();

            Assert.Equal(2, receivedProperties.Count);
            Assert.Collection(receivedProperties,
                item => Assert.Equal(KeyValuePair.Create<string, object?>(enrichedKey, enrichedData), item),
                item => Assert.Equal(KeyValuePair.Create<string, object?>("OriginalFormat", MessageSimple), item));
        }

        [Fact]
        public void Should_handle_DictionaryStringObject_scopes()
        {
            var services = CreateServiceProvider(x =>
            {
                x.IncludeScopes = true;
                x.IncludeCategoryName = false;
            });

            var receivedProperties = new Dictionary<string, object?>();
            SetupTrackTrace((x) => receivedProperties = x.Properties);

            var logger = services.GetRequiredService<ILogger<ApplicationInsightsLoggerTests>>();
            using var _ = logger.BeginScope(new Dictionary<string, object?> { ["Key1"] = "Val1", ["Key2"] = "Val2" });
            logger.LogInformation(MessageSimple);

            _appInsightsMock.Verify(x => x.TrackTrace(It.Is<TraceTelemetry>(trace => trace.Message == MessageSimple && trace.SeverityLevel == SeverityLevel.Information)),
                Times.Once);
            _appInsightsMock.VerifyNoOtherCalls();

            Assert.Equal(3, receivedProperties.Count);
            Assert.Collection(receivedProperties,
                item => Assert.Equal(KeyValuePair.Create<string, object?>("Key1", "Val1"), item),
                item => Assert.Equal(KeyValuePair.Create<string, object?>("Key2", "Val2"), item),
                item => Assert.Equal(KeyValuePair.Create<string, object?>("OriginalFormat", MessageSimple), item));
        }

        [Fact]
        public void Should_handle_format_as_expected()
        {
            const string originalFormat = "{SourceUserId} sent a message to {DestinationUserId}";
            const string formattedMessage = "1234 sent a message to 4321";

            var services = CreateServiceProvider(x =>
            {
                x.IncludeScopes = true;
                x.IncludeCategoryName = false;
            });

            var receivedProperties = new Dictionary<string, object?>();
            SetupTrackTrace((x) => receivedProperties = x.Properties);

            var logger = services.GetRequiredService<ILogger<ApplicationInsightsLoggerTests>>();
            logger.LogInformation(originalFormat, 1234, 4321);

            _appInsightsMock.Verify(x => x.TrackTrace(It.Is<TraceTelemetry>(trace => trace.Message == formattedMessage && trace.SeverityLevel == SeverityLevel.Information)),
                Times.Once);
            _appInsightsMock.VerifyNoOtherCalls();

            Assert.Equal(3, receivedProperties.Count);
            Assert.Collection(receivedProperties,
                item => Assert.Equal(KeyValuePair.Create<string, object?>("SourceUserId", "1234"), item),
                item => Assert.Equal(KeyValuePair.Create<string, object?>("DestinationUserId", "4321"), item),
                item => Assert.Equal(KeyValuePair.Create<string, object?>("OriginalFormat", originalFormat), item));
        }

        [Fact]
        public void Should_include_EventId_when_present()
        {
            var services = CreateServiceProvider();

            var receivedProperties = new Dictionary<string, object?>();
            SetupTrackTrace((x) => receivedProperties = x.Properties);

            var eventId = new EventId(1234, "Name");
            var logger = services.GetRequiredService<ILogger<ApplicationInsightsLoggerTests>>();
            logger.LogInformation(eventId, MessageSimple);

            _appInsightsMock.Verify(x => x.TrackTrace(It.Is<TraceTelemetry>(trace => trace.Message == MessageSimple && trace.SeverityLevel == SeverityLevel.Information)),
                Times.Once);
            _appInsightsMock.VerifyNoOtherCalls();

            Assert.Equal(3, receivedProperties.Count);
            Assert.Collection(receivedProperties,
                item => Assert.Equal(KeyValuePair.Create<string, object?>("EventId", $"{eventId.Id}"), item),
                item => Assert.Equal(KeyValuePair.Create<string, object?>("EventName", $"{eventId.Name}"), item),
                item => Assert.Equal(KeyValuePair.Create<string, object?>("OriginalFormat", MessageSimple), item)
            );
        }

        [Theory]
        [MemberData(nameof(LogLevelSeverityLevelExpectedMapping))]
        public void Should_write_messages_without_exceptions_to_trace_for_LogLevel(LogLevelSeverityLevelMapping mapping)
        {
            var services = CreateServiceProvider();

            var receivedProperties = new Dictionary<string, object?>();
            SetupTrackTrace((x) => receivedProperties = x.Properties);

            var logger = services.GetRequiredService<ILogger<ApplicationInsightsLoggerTests>>();
            logger.Log(mapping.LogLevel, MessageSimple);

            _appInsightsMock.Verify(x => x.TrackTrace(It.Is<TraceTelemetry>(trace => trace.Message == MessageSimple && trace.SeverityLevel == mapping.SeverityLevel)),
                Times.Once);
            _appInsightsMock.VerifyNoOtherCalls();

            Assert.Single(receivedProperties);
            Assert.Collection(receivedProperties,
                item => Assert.Equal(KeyValuePair.Create<string, object?>("OriginalFormat", MessageSimple), item)
            );
        }

        [Fact]
        public void Should_not_log_for_LogLevel_none()
        {
            var services = CreateServiceProvider();
            var logger = services.GetRequiredService<ILogger<ApplicationInsightsLoggerTests>>();
            logger.Log(LogLevel.None, MessageSimple);
            _appInsightsMock.VerifyNoOtherCalls();
        }

        private void SetupTrackTrace(Action<TraceTelemetry> callback)
        {
            _appInsightsMock
                .Setup(x => x.TrackTrace(It.IsAny<TraceTelemetry>()))
                .Callback(callback);
        }

        private void SetupTrackException(Action<ExceptionTelemetry> callback)
        {
            _appInsightsMock
                .Setup(x => x.TrackException(It.IsAny<ExceptionTelemetry>()))
                .Callback(callback);
        }

        private IServiceProvider CreateServiceProvider(Action<ApplicationInsightsLoggerOptions>? configure = null)
        {
            configure ??= options =>
            {
                options.IncludeScopes = false;
                options.IncludeCategoryName = false;
            };

            var services = new ServiceCollection();

            services.AddSingleton(_ => _jsRuntimeMock.Object);
            services.AddSingleton(_ => _appInsightsMock.Object);
            services.AddLogging(x => x.SetMinimumLevel(LogLevel.Trace));
            services.AddBlazorApplicationInsights(loggingOptions: configure);

            var provider = services.BuildServiceProvider();
            return provider;
        }

        public static readonly TheoryData<LogLevelSeverityLevelMapping> LogLevelSeverityLevelExpectedMapping = new TheoryData<LogLevelSeverityLevelMapping>
        {
            new LogLevelSeverityLevelMapping(LogLevel.Trace, SeverityLevel.Verbose),
            new LogLevelSeverityLevelMapping(LogLevel.Debug, SeverityLevel.Verbose),
            new LogLevelSeverityLevelMapping(LogLevel.Information, SeverityLevel.Information),
            new LogLevelSeverityLevelMapping(LogLevel.Warning, SeverityLevel.Warning),
            new LogLevelSeverityLevelMapping(LogLevel.Error, SeverityLevel.Error),
            new LogLevelSeverityLevelMapping(LogLevel.Critical, SeverityLevel.Critical),
            new LogLevelSeverityLevelMapping((LogLevel)999, SeverityLevel.Verbose),
        };

        public class LogLevelSeverityLevelMapping
        {
            public LogLevelSeverityLevelMapping(LogLevel logLevel, SeverityLevel severityLevel)
            {
                LogLevel = logLevel;
                SeverityLevel = severityLevel;
            }

            public LogLevel LogLevel { get; }
            public SeverityLevel SeverityLevel { get; }

            public override string ToString() => $"{LogLevel} maps to {SeverityLevel}";
        }
    }
}