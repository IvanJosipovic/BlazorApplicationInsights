window.blazorApplicationInsights = {
    addTelemetryInitializer: function (telemetryInitializer) {
        appInsights.addTelemetryInitializer(async (telemetryItem) => {
            return await telemetryInitializer.invokeMethodAsync("InvokeTelemetryInitializer", telemetryItem);
        });
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
    getUserId: function () {
        if (appInsights.context !== undefined) {
            return appInsights.context.user.authenticatedId || appInsights.context.user.id;
        }
    },
    getSessionId: function () {
        if (appInsights.context !== undefined) {
            return appInsights.context.sessionManager.automaticSession.id;
        }
    },
    setCookiesEnabled: function (enabled) {
        if (appInsights.core !== undefined) {
            appInsights.core.getCookieMgr().setEnabled(enabled);
        }
    },
    getCookiesEnabled: function () {
        if (appInsights.core !== undefined) {
            return appInsights.core.getCookieMgr().isEnabled();
        } else {
            return false;
        }
    },
};
