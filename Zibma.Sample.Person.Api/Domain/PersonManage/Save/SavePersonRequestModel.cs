using System.Text.Json.Serialization;
using Zibma.Sample.Person.Api.Common.Enum;

namespace Zibma.Sample.Person.Api.Domain.PersonManage.Save
{
    public class SavePersonRequestModel
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
        public eGender Gender { get; set; }
    }
}
