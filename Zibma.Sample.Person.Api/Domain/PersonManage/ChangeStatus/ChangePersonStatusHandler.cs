using BLL;
using MediatR;
using Zibma.MS.Common.Models.Exceptions;
using Zibma.Sample.Person.Api.Common.Enum;
using Zibma.Sample.Person.Api.Domain.HandlerBase;

namespace Zibma.Sample.Person.Api.Domain.PersonManage.ChangeStatus
{
    public class ChangePersonStatusHandler : ZibmaHandlerBase<ChangePersonStatusResponseModel>, IRequestHandler<ChangePersonStatusCommand, ChangePersonStatusResponseModel>
    {
        public ILogger<ChangePersonStatusHandler> _logger { get; }
        public ChangePersonStatusHandler(ILogger<ChangePersonStatusHandler> logger) : base(logger, nameof(ChangePersonStatusHandler))
        {
            _logger = logger;
        }

        public Task<ChangePersonStatusResponseModel> Handle(ChangePersonStatusCommand request, CancellationToken cancellationToken)
        => TryCatch(async () =>
        {
            if (request.PersonId <= 0)
                throw new ZibmaException("Invalid PersonId");


            var objPerson = (await PersonBLL.SelectList(new BOL.Person() { PersonId = request.PersonId })).FirstOrDefault();
            if (objPerson == null || objPerson.eStatus == (int)eStatus.Delete)
                throw new ZibmaException("No Data Found");


            if (objPerson.eStatus == (int)request.eStatus)
                throw new ZibmaException("Person is Already " + request.eStatus);


            await PersonBLL.Update(new BOL.Person()
            {
                PersonId = request.PersonId,
                eStatus = (int)request.eStatus,
                LastUpdateTime = DateTime.Now,
            });

            _logger.LogInformation("ActivityLog: {LogType}" +
                                     " by {LoginUserId}, " +
                                     " PersonId: {ItemId} " +
                                     " OldData: {OldData} " +
                                     " NewData: {NewData} " +
                                     " at {Time}",
                                     eLogType.Update.ToString(),
                                     0,        //Replace 0 with Current Login User Id
                                     request.PersonId,
                                     (eStatus)objPerson.eStatus!,
                                     request.eStatus,
                                     DateTime.Now);

            return new ChangePersonStatusResponseModel() { StatusCode = System.Net.HttpStatusCode.OK, ResponseMessage = "Person " + request.eStatus + " Successfully." };

        });
    }
}
