using Microsoft.AspNetCore.Mvc;
using Microsoft.Net.Http.Headers;
using System.Net;
using Zibma.MS.Common.Models.Exceptions;
using Zibma.Sample.Person.Api.Common.Abstractions;

namespace Zibma.Sample.Person.Api.Controllers.BaseContoller
{
    public class ZibmaControllerBase : ControllerBase
    {
        protected delegate Task<ActionResult> ControllerDelegate();
        protected async Task<ActionResult> TryCatch(ControllerDelegate model)
        {
            try
            {
                return await model();
            }
            catch (ZibmaException ex)
            {
                ApiResponseBase apiResponse = new ApiResponseBase() { StatusCode = ex.statusCode, Errors = ex.Errors };
                return ReturnResponseData(apiResponse);
            }
            catch (Exception ex)
            {
                ApiResponseBase apiResponse = new ApiResponseBase() { StatusCode = HttpStatusCode.InternalServerError, Errors = new List<string>() { ex.Message } };
                return ReturnResponseData(apiResponse);
            }

        }

        protected ActionResult ReturnResponseData(ApiResponseBase responseBase)
        {
            Response.StatusCode = (int)responseBase.StatusCode;
            return new JsonResult(responseBase);
        }

        protected string CurrentRequestAccessToken()
            => Request.Headers[HeaderNames.Authorization];
    }
}
