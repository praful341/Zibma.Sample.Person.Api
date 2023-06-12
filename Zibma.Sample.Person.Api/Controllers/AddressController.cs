using AutoMapper;
using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Zibma.Sample.Person.Api.Controllers.BaseContoller;
using Zibma.Sample.Person.Api.Domain.PersonManage.SaveAddress;

namespace Zibma.Sample.Person.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AddressController : ZibmaControllerBase
    {
        private IMediator _mediator { get; }
        private IMapper _mapper { get; }

        public AddressController(IMediator mediator, IMapper mapper)
        {
            _mediator = mediator;
            _mapper = mapper;
        }

        [HttpPost("save")]
        public Task<ActionResult> SaveAddress(SaveAddressRequestModel requestData)
     => TryCatch(async () =>
     {
         var command = _mapper.Map<SaveAddressCommand>(requestData);
         //command.Token = CurrentRequestAccessToken();
         var res = await _mediator.Send(command);
         return ReturnResponseData(res);
     });
    }
}
