using AutoMapper;
using BLL;
using BOL;
using MediatR;
using System.Net;
using Zibma.MS.Common.Enums;
using Zibma.MS.Common.Models.Exceptions;
using Zibma.Sample.Person.Api.Domain.HandlerBase;

namespace Zibma.Sample.Person.Api.Domain.PersonManage.GetAll
{
    public class GetAllPersonHandler : ZibmaHandlerBase<GetAllPersonResponseModel>, IRequestHandler<GetAllPersonCommand, GetAllPersonResponseModel>
    {
        private ILogger<GetAllPersonHandler> _logger { get; }

        public GetAllPersonHandler(ILogger<GetAllPersonHandler> logger, IMapper mapper) : base(logger, nameof(GetAllPersonHandler))
        {
            _logger = logger;
        }

        public Task<GetAllPersonResponseModel> Handle(GetAllPersonCommand request, CancellationToken cancellationToken)
        => TryCatch(async () =>
        {
            var lstPerson = await QueryBLL.ExeQuery<PersonDto>(new Query()
            {
                MasterSearch = request.MasterSearch,
                eStatus = (int)eStatus.Delete
            }, eQuery.Get_All_Person_Detail);

            if (!lstPerson.Any())
                throw new ZibmaException("No Data Found");

            return new GetAllPersonResponseModel() { StatusCode = HttpStatusCode.OK, lstPerson = lstPerson.ToList() };
        });
    }
}
