using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace BlazorApplicationInsights.Models.Context
{
    /// <summary>
    /// Automatic Session Manager
    /// Source: https://github.com/microsoft/ApplicationInsights-JS/blob/main/shared/AppInsightsCommon/src/Interfaces/Context/ISessionManager.ts
    /// </summary>
    public class SessionManager
    {
        /// <summary>
        /// The automatic Session which has been initialized from the automatic SDK cookies and storage.
        /// </summary>
        [JsonPropertyName("automaticSession")]
        public Session AutomaticSession { get; set; }

        /// <summary>
        /// Update the automatic session cookie if required.
        /// </summary>
        public async Task Update()
        {
            throw new NotImplementedException();
        }

        /// <summary>
        /// Record the current state of the automatic session and store it in our cookie string format
        /// into the browser's local storage. This is used to restore the session data when the cookie
        /// expires.
        /// </summary>
        public async Task Backup()
        {
            throw new NotImplementedException();
        }
    }
}
