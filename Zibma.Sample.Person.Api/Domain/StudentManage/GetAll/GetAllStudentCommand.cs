using MediatR;
using Zibma.Sample.Person.Api.Domain.PersonManage.GetAll;

namespace Zibma.Sample.Person.Api.Domain.StudentManage.GetAll
{
    public class GetAllStudentCommand : IRequest<GetAllStudentResponseModel>
    {
        public string MasterSearch { get; set; }
    }
}
