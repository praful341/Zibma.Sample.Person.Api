using AutoMapper;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using System.ComponentModel.DataAnnotations;
using Zibma.Sample.Person.Api.Common.Enum;
using Zibma.Sample.Person.Api.Controllers.BaseContoller;
using Zibma.Sample.Person.Api.Domain.PersonManage.GetAll;
using Zibma.Sample.Person.Api.Domain.PersonManage.GetById;
using Zibma.Sample.Person.Api.Domain.StudentManage.ChangeStatus;
using Zibma.Sample.Person.Api.Domain.StudentManage.GetAll;
using Zibma.Sample.Person.Api.Domain.StudentManage.GetById;
using Zibma.Sample.Person.Api.Domain.StudentManage.Save;

namespace Zibma.Sample.Person.Api.Controllers
{
    [ApiController]
    [Route("smaple/student")]
    
    public class StudentController : ZibmaControllerBase
    {
        private IMediator _mediator { get; }
        private IMapper _mapper { get; }

        public StudentController(IMediator mediator, IMapper mapper)
        {
            _mediator = mediator;
            _mapper = mapper;
        }

        [HttpPost("save")]
        public Task<ActionResult> SaveStudent(SaveStudentRequestModel requestData)
        => TryCatch(async () =>
        {
            var command = _mapper.Map<SaveStudentCommand>(requestData);
            //command.Token = CurrentRequestAccessToken();
            var res = await _mediator.Send(command);
            return ReturnResponseData(res);
        });

        [HttpPost("get_all")]
        public Task<ActionResult> GetAllStudent(GetAllStudentRequestModel requestData)
       => TryCatch(async () =>
       {
           var command = _mapper.Map<GetAllStudentCommand>(requestData);
           var res = await _mediator.Send(command);
           return ReturnResponseData(res);
       });

        [HttpGet("get_by_id")]
        public Task<ActionResult> GetStudentById([FromQuery, Required] int StudentId)
        => TryCatch(async () =>
        {
            GetStudentByIdResponseModel res = await _mediator.Send(new GetStudentByIdCommand() { StudentId = StudentId });
            return ReturnResponseData(res);
        });

        [HttpGet("change_status")]
        public Task<ActionResult> ChangeStudentStatus([FromQuery, Required] int StudentId, [FromQuery, Required] eStatus Status)
       => TryCatch(async () =>
       {
           ChangeStudentStatusResponseModel res = await _mediator.Send(new ChangeStudentStatusCommand { StudentId = StudentId, eStatus = Status });
           return ReturnResponseData(res);
       });

    }
}
