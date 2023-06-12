using MediatR;
using Zibma.Sample.Person.Api.Domain.PersonManage.GetById;

namespace Zibma.Sample.Person.Api.Domain.StudentManage.GetById
{
    public class GetStudentByIdCommand : IRequest<GetStudentByIdResponseModel>
    {
        public int StudentId { get; set; }
    }
}
