using MediatR;
using System.Net.Mail;
using System.Reflection;
using System.Security.Claims;
using Zibma.MS.Common.Enums;
using Zibma.Sample.Person.Api.Domain.PersonManage.Save;

namespace Zibma.Sample.Person.Api.Domain.StudentManage.Save
{
    public class SaveStudentCommand : IRequest<SaveStudentResponseModel>
    {
        public int StudentId { get; set; }
        public string StudentName { get; set; }
        public string FatherName { get; set; }
        public string CityName { get; set; }
        public eGender Gender { get; set; }
        public string Class { get; set; }
        public string RoleNo { get; set; }
        public string Mobile { get; set; }
        public string EmailAddress { get; set; }
        public decimal SchoolFees { get; set; }
        public decimal BusFees { get; set; }
        public string Address { get; set; }
    }
}
