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
                envelope.data = telemetryItem.data;
            }
            if (telemetryItem.baseType !== null) {
                envelope.baseType = telemetryItem.baseType;
            }
            if (telemetryItem.baseData !== null) {
                envelope.baseData = telemetryItem.baseData;
            }
        }
        appInsights.addTelemetryInitializer(telemetryInitializer);
    }
};