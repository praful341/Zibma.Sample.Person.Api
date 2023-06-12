using System.Text.Json.Serialization;
using Zibma.Sample.Person.Api.Common.Abstractions;

namespace Zibma.Sample.Person.Api.Domain.Version.UpdateNow
{
    public class UpdateNowResponseModel : ApiResponseBase
    {
        [JsonPropertyName("latestUpdate")]
        public DateTime LatestUpdate { get; set; }

        [JsonPropertyName("latestVersion")]
        public int LatestVersion { get; set; }
    }
}
