using MediatR;
using Zibma.Sample.Person.Api.Common.Enum;

namespace Zibma.Sample.Person.Api.Domain.PersonManage.ChangeStatus
{
    public class ChangePersonStatusCommand : IRequest<ChangePersonStatusResponseModel>
    {
        public int PersonId { get; set; }
        public eStatus eStatus { get; set; }
    }
}
