using System.Text.Json.Serialization;

namespace Zibma.Sample.Person.Api.Domain.PersonManage.GetAll
{
    public class GetAllPersonRequestModel
    {
        [JsonPropertyName("MasterSearch")]
        public string MasterSearch { get; set; }
    }
}
