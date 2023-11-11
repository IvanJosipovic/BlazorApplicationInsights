using BlazorApplicationInsights.Models;
using System.ComponentModel;
using System.Threading.Tasks;

namespace BlazorApplicationInsights.Interfaces;

[EditorBrowsable(EditorBrowsableState.Never)]
[Browsable(false)]
public interface IPropertiesPlugin
{
    Task<TelemetryContext> Context();
}
