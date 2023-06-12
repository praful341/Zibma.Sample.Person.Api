using AutoMapper;
using BLL;
using BOL;
using MediatR;
using System.Net;
using Zibma.MS.Common.Models.Exceptions;
using Zibma.Sample.Person.Api.Common.Enum;
using Zibma.Sample.Person.Api.Domain.HandlerBase;
using Zibma.Sample.Person.Api.Domain.PersonManage.GetAll;

namespace Zibma.Sample.Person.Api.Domain.StudentManage.GetAll
{
    public class GetAllStudentHandler : ZibmaHandlerBase<GetAllStudentResponseModel>, IRequestHandler<GetAllStudentCommand, GetAllStudentResponseModel>
    {
        private ILogger<GetAllStudentHandler> _logger { get; }

        public GetAllStudentHandler(ILogger<GetAllStudentHandler> logger, IMapper mapper) : base(logger, nameof(GetAllStudentHandler))
        {
            _logger = logger;
        }

        public Task<GetAllStudentResponseModel> Handle(GetAllStudentCommand request, CancellationToken cancellationToken)
        => TryCatch(async () =>
        {
            var lstStudent = await QueryBLL.ExeQuery<StudentDto>(new Query()
            {
                MasterSearch = request.MasterSearch,
                eStatus = (int)eStatus.Active
            }, eQuery.Get_All_Student_Detail);

            if (!lstStudent.Any())
                throw new ZibmaException("No Data Found");

            return new GetAllStudentResponseModel() { StatusCode = HttpStatusCode.OK, lstStudent = lstStudent.ToList() };
        });
    }
}
