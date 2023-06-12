using MediatR;
using Zibma.Sample.Person.Api.Common.Enum;

namespace Zibma.Sample.Person.Api.Domain.StudentManage.ChangeStatus
{
    public class ChangeStudentStatusCommand : IRequest<ChangeStudentStatusResponseModel>
    {
        public int StudentId { get; set; }
        public eStatus eStatus { get; set; }
    }
}
