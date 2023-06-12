using AutoMapper;
using BLL;
using MediatR;
using Zibma.MS.Common.Models.Exceptions;
using Zibma.Sample.Person.Api.Common.Enum;
using Zibma.Sample.Person.Api.Domain.HandlerBase;

namespace Zibma.Sample.Person.Api.Domain.PersonManage.GetById
{
    public class GetPersonByIdHandler : ZibmaHandlerBase<GetPersonByIdResponseModel>, IRequestHandler<GetPersonByIdCommand, GetPersonByIdResponseModel>
    {
        public ILogger<GetPersonByIdHandler> _logger { get; }
        public IMapper _mapper { get; }

        public GetPersonByIdHandler(ILogger<GetPersonByIdHandler> logger, IMapper mapper) : base(logger, nameof(GetPersonByIdHandler))
        {
            _logger = logger;
            _mapper = mapper;
        }

        public Task<GetPersonByIdResponseModel> Handle(GetPersonByIdCommand request, CancellationToken cancellationToken)
        => TryCatch(async () =>
        {
            if (request.PersonId <= 0)
                throw new ZibmaException("Invalid PersonId");

            var objPerson = (await PersonBLL.SelectList(new BOL.Person() { PersonId = request.PersonId })).FirstOrDefault();
            if (objPerson == null || objPerson.eStatus == (int)eStatus.Delete)
                throw new ZibmaException("No Data Found");

            var Response = _mapper.Map<GetPersonByIdResponseModel>(objPerson);
            Response.StatusCode = System.Net.HttpStatusCode.OK;
            return Response;
        });
    }
}
