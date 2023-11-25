using Microsoft.JSInterop;
using System.Threading.Tasks;

namespace BlazorApplicationInsights.Interfaces;

public class CookieMgr
{
    private readonly IJSRuntime JSRuntime;

    public CookieMgr(IJSRuntime jSRuntime)
    {
        JSRuntime = jSRuntime;
    }

    /// <summary>
    /// Enable or Disable the usage of cookies
    /// </summary>
    /// <param name="value">Value indicating whether cookies should be enabled</param>
    /// <returns>Task representing the asynchronous operation</returns>
    public async Task SetEnabled(bool value)
    {
        await JSRuntime.InvokeVoidAsync("blazorApplicationInsights.cookieMgrSetCookiesEnabled", value);
    }

    /// <summary>
    /// Can the system use cookies, if this returns false then all cookie setting and access functions will return nothing
    /// </summary>
    /// <returns>Task returning true if cookies are enabled, false otherwise</returns>
    public async Task<bool> IsEnabled()
    {
        return await JSRuntime.InvokeAsync<bool>("blazorApplicationInsights.cookieMgrGetCookiesEnabled");
    }

    /// <summary>
    /// Set the named cookie with the value and optional domain and optional
    /// </summary>
    /// <param name="name">The name of the cookie</param>
    /// <param name="value">The value of the cookie (Must already be encoded)</param>
    /// <param name="maxAgeSec">[optional] The maximum number of SECONDS that this cookie should survive</param>
    /// <param name="domain">[optional] The domain to set for the cookie</param>
    /// <param name="path">[optional] Path to set for the cookie, if not supplied will default to "/"</param>
    /// <returns>Task returning true if the cookie was set otherwise false (Because cookie usage is not enabled or available)</returns>
    public async Task<bool> Set(string name, string value, int? maxAgeSec = null, string? domain = null, string? path = null)
    {
        return await JSRuntime.InvokeAsync<bool>("blazorApplicationInsights.cookieMgrSet", name, value, maxAgeSec, domain, path);
    }

    /// <summary>
    /// Get the value of the named cookie
    /// </summary>
    /// <param name="name">The name of the cookie</param>
    /// <returns>Task returning the value of the named cookie</returns>
    public async Task<string> Get(string name)
    {
        return await JSRuntime.InvokeAsync<string>("blazorApplicationInsights.cookieMgrGet", name);
    }

    /// <summary>
    /// Delete/Remove the named cookie if cookie support is available and enabled.
    /// </summary>
    /// <param name="name">The name of the cookie</param>
    /// <param name="path">[optional] Path to set for the cookie, if not supplied will default to "/"</param>
    /// <returns>Task returning true if the cookie was marked for deletion otherwise false (Because cookie usage is not enabled or available)</returns>
    public async Task<bool> Del(string name, string? path = null)
    {
        return await JSRuntime.InvokeAsync<bool>("blazorApplicationInsights.cookieMgrDel", name, path);
    }

    /// <summary>
    /// Purge the cookie from the system if cookie support is available, this function ignores the enabled setting of the manager
    /// so any cookie will be removed.
    /// </summary>
    /// <param name="name">The name of the cookie</param>
    /// <param name="path">[optional] Path to set for the cookie, if not supplied will default to "/"</param>
    /// <returns>Task returning true if the cookie was marked for deletion otherwise false (Because cookie usage is not available)</returns>
    public async Task<bool> Purge(string name, string? path = null)
    {
        return await JSRuntime.InvokeAsync<bool>("blazorApplicationInsights.cookieMgrPurge", name, path);
    }

    /// <summary>
    /// Unload and remove any state that this ICookieMgr may be holding, this is generally called when the
    /// owning SDK is being unloaded.
    /// </summary>
    /// <param name="isAsync">Can the unload be performed asynchronously (default)</param>
    /// <returns>Task representing the asynchronous operation</returns>
    public async Task Unload(bool? isAsync)
    {
        await JSRuntime.InvokeVoidAsync("blazorApplicationInsights.cookieMgrUnload", isAsync);
    }
}