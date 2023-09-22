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