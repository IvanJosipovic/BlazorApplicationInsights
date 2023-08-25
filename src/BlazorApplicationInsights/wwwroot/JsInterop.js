window.blazorApplicationInsights = {
    addTelemetryInitializer: function (telemetryInitializer) {
        appInsights.addTelemetryInitializer(async (telemetryItem) => {
            return await telemetryInitializer.invokeMethodAsync("InvokeTelemetryInitializer", telemetryItem);
        });
    },
    getContext: function () {
        if (appInsights.context !== undefined) {
            return appInsights.context
        }
    }
};