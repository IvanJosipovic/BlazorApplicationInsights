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
                Object.assign(envelope.ext, telemetryItem.ext);
            }
            if (telemetryItem.tags !== null) {
                Object.assign(envelope.tags, telemetryItem.tags);
            }
            if (telemetryItem.data !== null) {
                Object.assign(envelope.data, telemetryItem.data);
            }
            if (telemetryItem.baseType !== null) {
                envelope.baseType = telemetryItem.baseType;
            }
            if (telemetryItem.baseData !== null) {
                Object.assign(envelope.baseData, telemetryItem.baseData);
            }
        }

        appInsights.addTelemetryInitializer(telemetryInitializer);
    },
    trackDependencyData: function (dependencyTelemetry) {
        if (dependencyTelemetry.startTime !== null) {
            dependencyTelemetry.startTime = new Date(dependencyTelemetry.startTime);
        }

        appInsights.trackDependencyData(dependencyTelemetry);
    },
    getContext: function () {
        if (appInsights.context !== undefined) {
            return appInsights.context
        }
    },
    cookieMgrSetCookiesEnabled: function (enabled) {
        if (appInsights.getCookieMgr !== undefined) {
            appInsights.getCookieMgr().setEnabled(enabled);
        }
    },
    cookieMgrGetCookiesEnabled: function () {
        if (appInsights.getCookieMgr !== undefined) {
            return appInsights.getCookieMgr().isEnabled();
        } else {
            return false;
        }
    },
    cookieMgrSet: function (name, value, maxAgeSec, domain, path) {
        if (appInsights.getCookieMgr !== undefined) {
            appInsights.getCookieMgr().set(name, value, maxAgeSec, domain, path);
        }
    },
    cookieMgrGet: function (name) {
        if (appInsights.getCookieMgr !== undefined) {
            appInsights.getCookieMgr().get(name);
        }
    },
    cookieMgrDel: function (name, path) {
        if (appInsights.getCookieMgr !== undefined) {
            appInsights.getCookieMgr().del(name, path);
        }
    },
    cookieMgrPurge: function (name, path) {
        if (appInsights.getCookieMgr !== undefined) {
            appInsights.getCookieMgr().purge(name, path);
        }
    },
    cookieMgrUnload: function (isAsync) {
        if (appInsights.getCookieMgr !== undefined) {
            appInsights.getCookieMgr().unload(isAsync);
        }
    },
};