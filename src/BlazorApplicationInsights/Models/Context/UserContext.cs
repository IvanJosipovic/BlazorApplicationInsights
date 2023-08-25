using System;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace BlazorApplicationInsights.Models.Context
{
    /// <summary>
    /// Source: https://github.com/microsoft/ApplicationInsights-JS/blob/main/shared/AppInsightsCommon/src/Interfaces/Context/IUser.ts
    /// </summary>
    public class UserContext
    {
        /// <summary>
        /// The telemetry configuration.
        /// </summary>
        [JsonPropertyName("config")]
        public object Config { get; set; }

        /// <summary>
        /// The user ID.
        /// </summary>
        [JsonPropertyName("id")]
        public string Id { get; set; }

        /// <summary>
        /// Authenticated user id.
        /// </summary>
        [JsonPropertyName("authenticatedId")]
        public string? AuthenticatedId { get; set; }

        /// <summary>
        /// The account ID.
        /// </summary>
        [JsonPropertyName("accountId")]
        public string? AccountId { get; set; }

        /// <summary>
        /// The account acquisition date.
        /// </summary>
        [JsonPropertyName("accountAcquisitionDate")]
        public string? AccountAcquisitionDate { get; set; }

        /// <summary>
        /// The localId.
        /// </summary>
        [JsonPropertyName("localId")]
        public string? LocalId { get; set; }

        /// <summary>
        /// A flag indicating whether this represents a new user.
        /// </summary>
        [JsonPropertyName("isNewUser")]
        public bool? IsNewUser { get; set; }

        /// <summary>
        /// A flag indicating whether the user cookie has been set.
        /// </summary>
        [JsonPropertyName("isUserCookieSet")]
        public bool? IsUserCookieSet { get; set; }

        //todo
        public async Task SetAuthenticatedUserContext(string authenticatedUserId, string? accountId = null, bool? storeInCookie = null)
        {
            throw new NotImplementedException();
        }

        //todo
        public async Task ClearAuthenticatedUserContext()
        {
            throw new NotImplementedException();
        }

        //todo
        public async Task Update(string? userId = null)
        {
            throw new NotImplementedException();
        }
    }
}
