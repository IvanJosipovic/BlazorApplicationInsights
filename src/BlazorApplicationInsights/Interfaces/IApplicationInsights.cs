using System.Threading.Tasks;
using BlazorApplicationInsights.Models;

namespace BlazorApplicationInsights.Interfaces;

/// <summary>
/// Application Insights API
/// Source:
/// https://github.com/microsoft/ApplicationInsights-JS/blob/main/AISKU/src/IApplicationInsights.ts
/// </summary>
public interface IApplicationInsights : IAppInsights, IDependenciesPlugin, IPropertiesPlugin
{
    /// <summary>
    /// Manually trigger an immediate send of all telemetry still in the buffer.
    /// </summary>
    /// <param name="async"></param>
    Task Flush(bool? async = null);

    /// <summary>
    /// Clears the authenticated user id and account id. The associated cookie is cleared, if present.
    /// </summary>
    Task ClearAuthenticatedUserContext();

    /// <summary>
    /// <para>Set the authenticated user id and the account id. Used for identifying a specific signed-in user. Parameters must not contain whitespace or ,;=|</para>
    /// <para>The method will only set the `authenticatedUserId` and `accountId` in the current page view. To set them for the whole session, you should set `storeInCookie = true`</para>
    ///
    /// </summary>
    /// <param name="authenticatedUserId"></param>
    /// <param name="accountId"></param>
    /// <param name="storeInCookie">Defaults to false</param>
    Task SetAuthenticatedUserContext(string authenticatedUserId, string? accountId = null, bool? storeInCookie = null);

    /// <summary>
    /// Update the configuration
    /// </summary>
    /// <param name="newConfig">The new configuration is apply</param>
    /// <param name="mergeExisting">Should the new configuration merge with the existing or just replace it. Default is to merge.</param>
    Task UpdateCfg(Config newConfig, bool? mergeExisting = true);
}