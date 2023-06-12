using MediatR;

namespace Zibma.Sample.Person.Api.Domain.PersonManage.GetAll
{
    public class GetAllPersonCommand: IRequest<GetAllPersonResponseModel>
    {
        public string MasterSearch { get; set; }
    }
}
