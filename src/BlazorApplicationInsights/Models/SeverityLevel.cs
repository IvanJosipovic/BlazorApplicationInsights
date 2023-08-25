namespace BlazorApplicationInsights.Models
{
    /// <summary>
    /// Defines the level of severity for the event.
    /// Source: https://github.com/microsoft/ApplicationInsights-JS/blob/main/shared/AppInsightsCommon/src/Interfaces/Contracts/SeverityLevel.ts
    /// </summary>
    public enum SeverityLevel
    {
        /// <summary>
        /// Verbose
        /// </summary>
        Verbose = 0,

        /// <summary>
        /// Information
        /// </summary>
        Information = 1,

        /// <summary>
        /// Warning
        /// </summary>
        Warning = 2,

        /// <summary>
        /// Error
        /// </summary>
        Error = 3,

        /// <summary>
        /// Critical
        /// </summary>
        Critical = 4,
    }
}