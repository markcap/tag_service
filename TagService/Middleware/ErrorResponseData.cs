using System.Net;
using System.Text.Json.Serialization;

namespace TagService.Middleware
{
    public class ErrorResponseData
    {
        /// <summary>Gets or sets the status.</summary>
        /// <value>The HttpStatusCode.</value>
        [JsonPropertyName("status")]
        public HttpStatusCode Status { get; set; }

        [JsonPropertyName("message")]
        public string Message { get; set; }

        [JsonPropertyName("stackTrace")]
        public string StackTrace { get; set; }
    }
}