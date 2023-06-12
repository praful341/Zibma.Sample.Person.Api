using AutoMapper;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using System.ComponentModel.DataAnnotations;
using Zibma.Sample.Person.Api.Common.Enum;
using Zibma.Sample.Person.Api.Controllers.BaseContoller;
using Zibma.Sample.Person.Api.Domain.PersonManage.ChangeStatus;
using Zibma.Sample.Person.Api.Domain.PersonManage.GetAll;
using Zibma.Sample.Person.Api.Domain.PersonManage.GetById;
using Zibma.Sample.Person.Api.Domain.PersonManage.Save;

namespace Zibma.Sample.Person.Api.Controllers
{
    [ApiController]
    [Route("smaple/person")]
    public class PersonController : ZibmaControllerBase
    {
        private IMediator _mediator { get; }
        private IMapper _mapper { get; }

        public PersonController(IMediator mediator, IMapper mapper)
        {
            _mediator = mediator;
            _mapper = mapper;
        }

        [HttpPost("save")]
        public Task<ActionResult> SaveDesignation(SavePersonRequestModel requestData)
        => TryCatch(async () =>
        {
            var command = _mapper.Map<SavePersonCommand>(requestData);
            //command.Token = CurrentRequestAccessToken();
            var res = await _mediator.Send(command);
            return ReturnResponseData(res);
        });

        [HttpPost("get_all")]
        public Task<ActionResult> GetAllPerson(GetAllPersonRequestModel requestData)
        => TryCatch(async () =>
        {
            var command = _mapper.Map<GetAllPersonCommand>(requestData);
            var res = await _mediator.Send(command);
            return ReturnResponseData(res);
        });

        [HttpGet("get_by_id")]
        public Task<ActionResult> GetPersonById([FromQuery, Required] int PersonId)
        => TryCatch(async () =>
        {
            GetPersonByIdResponseModel res = await _mediator.Send(new GetPersonByIdCommand() { PersonId = PersonId });
            return ReturnResponseData(res);
        });

        [HttpGet("change_status")]
        public Task<ActionResult> ChangePersonStatus([FromQuery, Required] int PersonId, [FromQuery, Required] eStatus Status)
        => TryCatch(async () =>
        {
            ChangePersonStatusResponseModel res = await _mediator.Send(new ChangePersonStatusCommand() { PersonId = PersonId, eStatus = Status });
            return ReturnResponseData(res);
        });
    }
}
