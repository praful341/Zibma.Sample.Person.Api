using MediatR;
using Microsoft.AspNetCore.Mvc;
using Zibma.Sample.Person.Api.Domain.Version.UpdateNow;

namespace Zibma.Sample.Person.Api.Controllers
{
    [ApiController]
    [Route("version")]
    public class VersionController : ControllerBase
    {
        private IMediator _mediator { get; }

        public VersionController(IMediator mediator)
        {
            _mediator = mediator;
        }

        [HttpGet("update_now")]
        public async Task<ActionResult> UpdateNow()
        {
            var res = await _mediator.Send(new UpdateNowCommand());
            Response.StatusCode = (int)res.StatusCode;
            return new JsonResult(res);
        }

    }
}
