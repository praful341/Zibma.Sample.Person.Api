using System.Text.Json.Serialization;
using Zibma.Sample.Person.Api.Common.Abstractions;

namespace Zibma.Sample.Person.Api.Domain.PersonManage.GetById
{
    public class GetPersonByIdResponseModel : ApiResponseBase
    {
        [JsonPropertyName("PersonId")]
        public int PersonId { get; set; }

        [JsonPropertyName("FirstName")]
        public string FirstName { get; set; }

        [JsonPropertyName("LastName")]
        public string LastName { get; set; }

        [JsonPropertyName("Mobile")]
        public string Mobile { get; set; }

        [JsonPropertyName("Email")]
        public string Email { get; set; }
    }
}
