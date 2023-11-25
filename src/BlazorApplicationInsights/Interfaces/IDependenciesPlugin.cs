using BlazorApplicationInsights.Models;
using System.ComponentModel;
using System.Threading.Tasks;

namespace BlazorApplicationInsights.Interfaces;

/// <summary>
/// Dependencies Plugin
/// Source:
/// https://github.com/microsoft/ApplicationInsights-JS/blob/main/extensions/applicationinsights-dependencies-js/src/ajax.ts#L232
/// </summary>
[EditorBrowsable(EditorBrowsableState.Never)]
[Browsable(false)]
public interface IDependenciesPlugin
{
    /// <summary>
    /// Logs dependency call
    /// </summary>
    /// <param name="dependency">dependency data object</param>
    /// <returns></returns>
    Task TrackDependencyData(DependencyTelemetry dependency);
}
