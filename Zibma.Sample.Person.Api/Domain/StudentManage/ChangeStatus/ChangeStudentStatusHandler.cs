using BLL;
using MediatR;
using Zibma.MS.Common.Models.Exceptions;
using Zibma.Sample.Person.Api.Common.Enum;
using Zibma.Sample.Person.Api.Domain.HandlerBase;
using Zibma.Sample.Person.Api.Domain.PersonManage.ChangeStatus;

namespace Zibma.Sample.Person.Api.Domain.StudentManage.ChangeStatus
{
    public class ChangeStudentStatusHandler : ZibmaHandlerBase<ChangeStudentStatusResponseModel>, IRequestHandler<ChangeStudentStatusCommand, ChangeStudentStatusResponseModel>
    {
        public ILogger<ChangeStudentStatusHandler> _logger { get; }
        public ChangeStudentStatusHandler(ILogger<ChangeStudentStatusHandler> logger) : base(logger, nameof(ChangeStudentStatusHandler))
        {
            _logger = logger;
        }

        public Task<ChangeStudentStatusResponseModel> Handle(ChangeStudentStatusCommand request, CancellationToken cancellationToken)
        => TryCatch(async () =>
        {
            if (request.StudentId <= 0)
                throw new ZibmaException("Invalid StudentId");

            //string aa = await DAL.BusinessObject.GenerateClass("", "");

            var objPerson = (await StudentBLL.SelectList(new BOL.Student() { StudentId = request.StudentId })).FirstOrDefault();
            if (objPerson == null || objPerson.eStatus == (int)eStatus.Delete)
                throw new ZibmaException("No Data Found");


            if (objPerson.eStatus == (int)request.eStatus)
                throw new ZibmaException("Student is Already " + request.eStatus);


            await StudentBLL.Update(new BOL.Student()
            {
                StudentId = request.StudentId,
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
                                     request.StudentId,
                                     (eStatus)objPerson.eStatus!,
                                     request.eStatus,
                                     DateTime.Now);

            return new ChangeStudentStatusResponseModel() { StatusCode = System.Net.HttpStatusCode.OK, ResponseMessage = "Person " + request.eStatus + " Successfully." };

        });
    }
}
