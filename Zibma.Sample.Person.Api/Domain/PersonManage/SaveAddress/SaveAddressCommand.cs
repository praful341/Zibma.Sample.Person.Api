using MediatR;
using Zibma.Sample.Person.Api.Domain.PersonManage.Save;

namespace Zibma.Sample.Person.Api.Domain.PersonManage.SaveAddress
{
    public class SaveAddressCommand : IRequest<SaveAddressResponseModel>
    {
        public int AddressId { get; set; }
        public string AddressName { get; set; }
    }
}
