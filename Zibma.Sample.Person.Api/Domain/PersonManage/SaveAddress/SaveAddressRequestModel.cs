using System.Text.Json.Serialization;

namespace Zibma.Sample.Person.Api.Domain.PersonManage.SaveAddress
{
    public class SaveAddressRequestModel
    {
        [JsonPropertyName("AddressId")]
        public int AddressId { get; set; }

        [JsonPropertyName("AddressName")]
        public string AddressName { get; set; }
    }
}
