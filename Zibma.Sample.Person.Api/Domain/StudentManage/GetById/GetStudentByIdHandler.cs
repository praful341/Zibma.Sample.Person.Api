using AutoMapper;
using BLL;
using MediatR;
using Zibma.MS.Common.Models.Exceptions;
using Zibma.Sample.Person.Api.Common.Enum;
using Zibma.Sample.Person.Api.Domain.HandlerBase;
using Zibma.Sample.Person.Api.Domain.PersonManage.GetById;

namespace Zibma.Sample.Person.Api.Domain.StudentManage.GetById
{
    public class GetStudentByIdHandler : ZibmaHandlerBase<GetStudentByIdResponseModel>, IRequestHandler<GetStudentByIdCommand, GetStudentByIdResponseModel>
    {
        public ILogger<GetStudentByIdHandler> _logger { get; }
        public IMapper _mapper { get; }

        public GetStudentByIdHandler(ILogger<GetStudentByIdHandler> logger, IMapper mapper) : base(logger, nameof(GetStudentByIdHandler))
        {
            _logger = logger;
            _mapper = mapper;
        }

        public Task<GetStudentByIdResponseModel> Handle(GetStudentByIdCommand request, CancellationToken cancellationToken)
        => TryCatch(async () =>
        {
            if (request.StudentId <= 0)
                throw new ZibmaException("Invalid StudentId");

            var objStudent = (await StudentBLL.SelectList(new BOL.Student() { StudentId = request.StudentId })).FirstOrDefault();
            if (objStudent == null || objStudent.eStatus == (int)eStatus.Delete)
                throw new ZibmaException("No Data Found");

            var Response = _mapper.Map<GetStudentByIdResponseModel>(objStudent);
            Response.StatusCode = System.Net.HttpStatusCode.OK;
            return Response;
        });
    }
}
