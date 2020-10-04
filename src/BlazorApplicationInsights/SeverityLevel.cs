using Microsoft.JSInterop;
using System.Threading.Tasks;

namespace BlazorApplicationInsights
{
    /// <summary>
    /// Defines the level of severity for the event.
    /// </summary>
    public enum SeverityLevel
    {
        Verbose = 0,
        Information = 1,
        Warning = 2,
        Error = 3,
        Critical = 4,
    }
}