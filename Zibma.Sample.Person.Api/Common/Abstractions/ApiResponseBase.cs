using System.Net;
using System.Text.Json.Serialization;

namespace Zibma.Sample.Person.Api.Common.Abstractions
{
    public class ApiResponseBase
    {
        [JsonPropertyName("statusCode")]
        public HttpStatusCode StatusCode { get; set; } = HttpStatusCode.Unused;


        [JsonPropertyName("responseMessage")]
        public string ResponseMessage { get; set; } = null;


        [JsonPropertyName("errors")]
        public List<string> Errors { get; set; } = null;
    }
}
