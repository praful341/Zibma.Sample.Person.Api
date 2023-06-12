using AutoMapper;
using Zibma.Sample.Person.Api.Domain.PersonManage.GetAll;
using Zibma.Sample.Person.Api.Domain.PersonManage.GetById;
using Zibma.Sample.Person.Api.Domain.PersonManage.Save;
using Zibma.Sample.Person.Api.Domain.PersonManage.SaveAddress;
using Zibma.Sample.Person.Api.Domain.StudentManage.GetAll;
using Zibma.Sample.Person.Api.Domain.StudentManage.GetById;
using Zibma.Sample.Person.Api.Domain.StudentManage.Save;

namespace Zibma.Sample.Person.Api
{
    public class MappingProfile : Profile
    {
        public MappingProfile()
        {
            CreateMap<SavePersonRequestModel, SavePersonCommand>();
            CreateMap<SavePersonCommand, BOL.Person>();

            CreateMap<GetAllPersonRequestModel, GetAllPersonCommand>();
            CreateMap<BOL.Person, GetPersonByIdResponseModel>();

            CreateMap<SaveAddressRequestModel, SaveAddressCommand>();
            CreateMap<SaveAddressCommand, BOL.Address>();

            CreateMap<SaveStudentRequestModel, SaveStudentCommand>();
            CreateMap<SaveStudentCommand, BOL.Student>();

            CreateMap<GetAllStudentRequestModel, GetAllStudentCommand>();
            CreateMap<BOL.Student, GetStudentByIdResponseModel>();
        }
    }
}
