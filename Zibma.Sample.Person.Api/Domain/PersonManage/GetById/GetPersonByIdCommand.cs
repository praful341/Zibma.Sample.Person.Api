using MediatR;

namespace Zibma.Sample.Person.Api.Domain.PersonManage.GetById
{
    public class GetPersonByIdCommand: IRequest<GetPersonByIdResponseModel>
    {
        public int PersonId { get; set; }
    }
}
