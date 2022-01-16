window.blazorApplicationInsights = {
    addTelemetryInitializer: function (telemetryItem) {
        var telemetryInitializer = (envelope) => {
            if (telemetryItem.ver !== null) {
                envelope.ver = telemetryItem.ver;
            }
            if (telemetryItem.name !== null) {
                envelope.name = telemetryItem.name;
            }
            if (telemetryItem.time !== null) {
                envelope.time = telemetryItem.time;
            }
            if (telemetryItem.iKey !== null) {
                envelope.iKey = telemetryItem.iKey;
            }
            if (telemetryItem.ext !== null) {
                envelope.ext = telemetryItem.ext;
            }
            if (telemetryItem.tags !== null) {
                envelope.tags = telemetryItem.tags;
            }
            if (telemetryItem.data !== null) {
                Object.assign(envelope.data, telemetryItem.data);
            }
            if (telemetryItem.baseType !== null) {
                envelope.baseType = telemetryItem.baseType;
            }
            if (telemetryItem.baseData !== null) {
                envelope.baseData = telemetryItem.baseData;
            }
        }
        appInsights.addTelemetryInitializer(telemetryInitializer);
    },
    trackDependencyData: function (data) {
        var dependencyTelemetry = {};

        if (data.id !== null) {
            dependencyTelemetry["id"] = data.id;
        }
        if (data.name !== null) {
            dependencyTelemetry["name"] = data.name;
        }
        if (data.duration !== null) {
            dependencyTelemetry["duration"] = data.duration;
        }
        if (data.success !== null) {
            dependencyTelemetry["success"] = data.success;
        }
        if (data.startTime !== null) {
            dependencyTelemetry["startTime"] = new Date(data.startTime);
        }
        if (data.responseCode !== null) {
            dependencyTelemetry["responseCode"] = data.responseCode;
        }
        if (data.correlationContext !== null) {
            dependencyTelemetry["correlationContext"] = data.correlationContext;
        }
        if (data.type !== null) {
            dependencyTelemetry["type"] = data.type;
        }
        if (data.data !== null) {
            dependencyTelemetry["data"] = data.data;
        }
        if (data.target !== null) {
            dependencyTelemetry["target"] = data.target;
        }

        appInsights.trackDependencyData(dependencyTelemetry);
    },
    setInstrumentationKey: function (instrumentationKey) {
        appInsights.config.instrumentationKey = instrumentationKey;
    },
    setConnectionString: function (connectionString) {
        appInsights.config.connectionString = connectionString;
    },
    loadAppInsights: function () {
        if (appInsights.loadAppInsights !== undefined) {
            appInsights.loadAppInsights();
        }
    },
};