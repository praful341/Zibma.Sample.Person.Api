using AutoMapper;
using BLL;
using MediatR;
using System.Net;
using Zibma.MS.Common.Models.Exceptions;
using Zibma.Sample.Person.Api.Common.Enum;
using Zibma.Sample.Person.Api.Domain.HandlerBase;

namespace Zibma.Sample.Person.Api.Domain.PersonManage.Save
{
    public class SavePersonCommandHandler : ZibmaHandlerBase<SavePersonResponseModel>, IRequestHandler<SavePersonCommand, SavePersonResponseModel>
    {
        private ILogger<SavePersonCommandHandler> _logger { get; }
        private IMapper _mapper { get; }

        public SavePersonCommandHandler(ILogger<SavePersonCommandHandler> logger, IMapper mapper) : base(logger, nameof(SavePersonCommandHandler))
        {
            _logger = logger;
            _mapper = mapper;
        }

        public Task<SavePersonResponseModel> Handle(SavePersonCommand request, CancellationToken cancellationToken)
        => TryCatch(async () =>
        {
            #region validation

            var dbPerson = (await PersonBLL.SelectList(new BOL.Person() { PersonId = request.PersonId })).FirstOrDefault();
            if (request.PersonId > 0 && dbPerson == null)
                throw new ZibmaException("Invalid PersonId");

            var lstPerson = await QueryBLL.ExeQuery<PersonDto>(new BOL.Query() { Email = request.Email, eStatus = (int)eStatus.Delete }, eQuery.Get_Person_Email_For_Unique_Validation);
            if (lstPerson.Count() > 0 && lstPerson.First().PersonId != request.PersonId)
                throw new ZibmaException("Email Address Already Exists" + (lstPerson.First().eStatus == (int)eStatus.Deactive ? ((eStatus)lstPerson.First().eStatus).ToString() : "") + ".");

            #endregion

            string msg = "";
            var objPerson = _mapper.Map<BOL.Person>(request);
            objPerson.LastUpdateTime = DateTime.Now;

            if (request.PersonId == 0)
            {
                #region Insert Mode

                objPerson.eStatus = (int)eStatus.Active;
                objPerson.InsertTime = DateTime.Now;
                int NewPersonId = await PersonBLL.InsertId(objPerson);
                msg = "Person Inserted Successfully.";

                _logger.LogInformation("ActivityLog: {LogType}" +
                                      " by {LoginUserId}, " +
                                      " PersonId: {ItemId} " +
                                      " NewData: {NewData} " +
                                      " at {Time}",
                                      eLogType.Insert.ToString(),
                                      0,        //Replace 0 with Current Login User Id
                                      NewPersonId,
                                      "FirstName: " + objPerson.FirstName + " LastName: " + objPerson.LastName + " Mobile: " + objPerson.Mobile + " Email: " + objPerson.Email + " eGender:" + (eGender)objPerson.Gender!,
                                      DateTime.Now);

                #endregion
            }
            else
            {
                #region Update Mode

                await PersonBLL.Update(objPerson);
                msg = "Person Updated Successfully.";

                _logger.LogInformation("ActivityLog: {LogType}" +
                                    " by {LoginUserId}, " +
                                    " PersonId: {ItemId} " +
                                    " OldData: {OldData} " +
                                    " NewData: {NewData} " +
                                    " at {Time}",
                                    eLogType.Update.ToString(),
                                    0,        //Replace 0 with Current Login User Id
                                    objPerson.PersonId,
                                    "FirstName: " + dbPerson.FirstName + " LastName: " + dbPerson.LastName + " Mobile: " + dbPerson.Mobile + " Email: " + dbPerson.Email + " eGender:" + (eGender)dbPerson.Gender!,
                                    "FirstName: " + objPerson.FirstName + " LastName: " + objPerson.LastName + " Mobile: " + objPerson.Mobile + " Email: " + objPerson.Email + " eGender:" + (eGender)objPerson.Gender!,
                                    DateTime.Now);

                #endregion
            }

            return new SavePersonResponseModel() { StatusCode = HttpStatusCode.OK, ResponseMessage = msg };
        });

        public class PersonDto
        {
            public int PersonId { get; set; }
            public string Email { get; set; }
            public int eStatus { get; set; }
        }
    }
}
