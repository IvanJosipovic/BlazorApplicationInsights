using Microsoft.JSInterop;
using System;

namespace BlazorApplicationInsights.Models;

internal class TelemetryInitializer
{
    private readonly Func<TelemetryItem, bool> TelemetryItem;

    public TelemetryInitializer(Func<TelemetryItem, bool> telemetryInitializer)
    {
        this.TelemetryItem = telemetryInitializer;
    }

    [JSInvokable("InvokeTelemetryInitializer")]
    public bool Invoke(TelemetryItem Item)
    {
        return TelemetryItem.Invoke(Item);
    }
}
