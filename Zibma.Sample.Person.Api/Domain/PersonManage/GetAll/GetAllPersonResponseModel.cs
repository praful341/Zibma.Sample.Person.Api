using System.Text.Json.Serialization;
using Zibma.Sample.Person.Api.Common.Abstractions;

namespace Zibma.Sample.Person.Api.Domain.PersonManage.GetAll
{
    public class GetAllPersonResponseModel : ApiResponseBase
    {
        [JsonPropertyName("lstPerson")]
        public List<PersonDto> lstPerson { get; set; }
    }

    public class PersonDto
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

        [JsonPropertyName("Gender")]
        public string Gender { get; set; }
    }
}
