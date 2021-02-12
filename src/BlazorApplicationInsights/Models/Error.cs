using System.Text.Json.Serialization;

namespace BlazorApplicationInsights
{
    public class Error
    {
        [JsonPropertyName("name")]
        public string Name { get; set; }

        [JsonPropertyName("message")]
        public string Message { get; set; }

        [JsonPropertyName("stack")]
        public string? Stack { get; set; }
    }
}