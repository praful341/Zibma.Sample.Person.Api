using MediatR;
using Zibma.Sample.Person.Api.Common.Enum;

namespace Zibma.Sample.Person.Api.Domain.PersonManage.Save
{
    public class SavePersonCommand: IRequest<SavePersonResponseModel>
    {
        public int PersonId { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Mobile { get; set; }
        public string Email { get; set; }
        public eGender Gender { get; set; }
    }
}
