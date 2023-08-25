window.blazorApplicationInsights = {
    addTelemetryInitializer: function (telemetryInitializer) {
        appInsights.addTelemetryInitializer(async (telemetryItem) => {
            return await telemetryInitializer.invokeMethodAsync("InvokeTelemetryInitializer", telemetryItem);
        });
    }
};
