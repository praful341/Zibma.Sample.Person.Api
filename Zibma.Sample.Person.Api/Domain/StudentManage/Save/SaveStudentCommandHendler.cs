using AutoMapper;
using BLL;
using MediatR;
using System.Net;
using Zibma.MS.Common.Enums;
using Zibma.MS.Common.Models.Exceptions;
using Zibma.Sample.Person.Api.Domain.HandlerBase;
using Zibma.Sample.Person.Api.Domain.PersonManage.GetAll;
using Zibma.Sample.Person.Api.Domain.PersonManage.Save;

namespace Zibma.Sample.Person.Api.Domain.StudentManage.Save
{
    public class SaveStudentCommandHendler : ZibmaHandlerBase<SaveStudentResponseModel>, IRequestHandler<SaveStudentCommand, SaveStudentResponseModel>
    {
        private ILogger<SaveStudentCommandHendler> _logger { get; }
        private IMapper _mapper { get; }

        public SaveStudentCommandHendler(ILogger<SaveStudentCommandHendler> logger, IMapper mapper) : base(logger, nameof(SaveStudentCommandHendler))
        {
            _logger = logger;
            _mapper = mapper;
        }

        public Task<SaveStudentResponseModel> Handle(SaveStudentCommand request, CancellationToken cancellationToken)
        => TryCatch(async () =>
        {
            //#region validation

            var dbStudent = (await StudentBLL.SelectList(new BOL.Student() { StudentId = request.StudentId })).FirstOrDefault();

            if (request.StudentId > 0 && dbStudent == null)
                throw new ZibmaException("Invalid StudentId");

            var lstStudent = await QueryBLL.ExeQuery<StudentDto>(new BOL.Query() { Email = request.EmailAddress, eStatus = (int)eStatus.Delete }, eQuery.Get_Person_Email_For_Unique_Validation);
            if (lstStudent.Count() > 0 && lstStudent.First().StudentId != request.StudentId)
                throw new ZibmaException("Email Address Already Exists" + (lstStudent.First().eStatus == (int)eStatus.Deactive ? ((eStatus)lstStudent.First().eStatus).ToString() : "") + ".");

            //#endregion

            string msg = "";
            var objStudent = _mapper.Map<BOL.Student>(request);
            objStudent.LastUpdateTime = DateTime.Now;

            if (request.StudentId == 0)
            {
                #region Insert Mode

                objStudent.eStatus = (int)eStatus.Active;
                objStudent.InsertTime = DateTime.Now;
                int NewStudentId = await StudentBLL.InsertId(objStudent);
                msg = "Student Inserted Successfully.";

                _logger.LogInformation("ActivityLog: {LogType}" +
                                      " by {LoginUserId}, " +
                                      " PersonId: {ItemId} " +
                                      " NewData: {NewData} " +
                                      " StudentName: {StudentName}" +
                                      " at {Time}",
                                      eLogType.Insert.ToString(),
                                      0,//Replace 0 with Current Login User Id
                                      NewStudentId,
                                      objStudent.StudentName,
                                      DateTime.Now
                                      );

                #endregion
            }
            else
            {
                #region Update Mode

                await StudentBLL.Update(objStudent);
                msg = "Student Updated Successfully.";

                _logger.LogInformation("ActivityLog: {LogType}" +
                                    " by {LoginUserId}, " +
                                    " PersonId: {ItemId} " +
                                    " StudentName: {StudentName} " +
                                    " at {Time}",
                                    eLogType.Update.ToString(),
                                    0,        //Replace 0 with Current Login User Id
                                    objStudent.StudentId,
                                    objStudent.StudentName,
                                    DateTime.Now
                                    );

                #endregion
            }

            return new SaveStudentResponseModel() { StatusCode = HttpStatusCode.OK, ResponseMessage = msg };
        });

        public class StudentDto
        {
            public int StudentId { get; set; }
            public string EmailAddress { get; set; }
            public int eStatus { get; set; }
        }
    }
}
