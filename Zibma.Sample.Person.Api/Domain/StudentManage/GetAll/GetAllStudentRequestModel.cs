using System.Text.Json.Serialization;

namespace Zibma.Sample.Person.Api.Domain.StudentManage.GetAll
{
    public class GetAllStudentRequestModel
    {
        [JsonPropertyName("MasterSearch")]
        public string MasterSearch { get; set; }
    }
}
