using AutoMapper;
using BLL;
using MediatR;
using Microsoft.AspNetCore.Http;
using System.Net;
using Zibma.MS.Common.Models.Exceptions;
using Zibma.Sample.Person.Api.Domain.PersonManage.Save;
using Zibma.Sample.Person.Api.Common.Enum;
using Zibma.Sample.Person.Api.Domain.HandlerBase;

namespace Zibma.Sample.Person.Api.Domain.PersonManage.SaveAddress
{
    public class SaveAddressCommandHandler : ZibmaHandlerBase<SaveAddressResponseModel>, IRequestHandler<SaveAddressCommand, SaveAddressResponseModel>
    {
        private ILogger<SaveAddressCommandHandler> _logger { get; }
        private IMapper _mapper { get; }

        public SaveAddressCommandHandler(ILogger<SaveAddressCommandHandler> logger, IMapper mapper) : base(logger, nameof(SaveAddressCommandHandler))
        {
            _logger = logger;
            _mapper = mapper;
        }

        public Task<SaveAddressResponseModel> Handle(SaveAddressCommand request, CancellationToken cancellationToken)
        => TryCatch(async () =>
        {
            string msg = "";
            var objPerson = _mapper.Map<BOL.Address>(request);
            objPerson.LastUpdateTime = DateTime.Now;

            if (request.AddressId == 0)
            {
                #region Insert Mode

                //objPerson.eStatus = (int)eStatus.Active;
                //objPerson.InsertTime = DateTime.Now;
                int NewPersonId = await AddressBLL.InsertId(objPerson);
                msg = "Person Inserted Successfully.";

                _logger.LogInformation("ActivityLog: {LogType}" +
                                      " by {LoginUserId}, " +
                                      " PersonId: {ItemId} " +
                                      " NewData: {NewData} " +
                                      " at {Time}",
                                      eLogType.Insert.ToString(),
                                      0,        //Replace 0 with Current Login User Id
                                      NewPersonId,
                                      "",
                                      DateTime.Now);

                #endregion
            }
            return new SaveAddressResponseModel() { StatusCode = HttpStatusCode.OK, ResponseMessage = msg };
        });

        public class AddressDto
        {
            public int AddressId { get; set; }
            public string AddressName { get; set; }
            public int eStatus { get; set; }
        }
    }
}
