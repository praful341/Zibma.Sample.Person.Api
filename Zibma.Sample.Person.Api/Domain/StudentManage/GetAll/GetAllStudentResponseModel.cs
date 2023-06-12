using System.Text.Json.Serialization;
using Zibma.Sample.Person.Api.Common.Abstractions;

namespace Zibma.Sample.Person.Api.Domain.StudentManage.GetAll
{
    public class GetAllStudentResponseModel : ApiResponseBase
    {
        [JsonPropertyName("lstStudent")]
        public List<StudentDto> lstStudent { get; set; }
    }

    public class StudentDto
    {

        [JsonPropertyName("Studentid")]
        public int StudentId { get; set; }

        [JsonPropertyName("StudentName")]
        public string StudentName { get; set; }

        [JsonPropertyName("FatherName")]
        public string FatherName { get; set; }

        [JsonPropertyName("CityName")]
        public string CityName { get; set; }

        [JsonPropertyName("Gender")]
        public string Gender { get; set; }

        [JsonPropertyName("Class")]
        public string Class { get; set; }

        [JsonPropertyName("RoleNo")]
        public string RoleNo { get; set; }

        [JsonPropertyName("Mobile")]
        public string Mobile { get; set; }

        [JsonPropertyName("EmailAddress")]
        public string EmailAddress { get; set; }

        [JsonPropertyName("SchoolFees")]
        public decimal SchoolFees { get; set; }

        [JsonPropertyName("BusFees")]
        public decimal BusFees { get; set; }

        [JsonPropertyName("Address")]
        public string Address { get; set; }
    }
}
