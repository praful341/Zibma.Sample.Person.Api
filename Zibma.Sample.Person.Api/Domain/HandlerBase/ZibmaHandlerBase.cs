using Zibma.MS.Common.Models.Exceptions;
using Zibma.Sample.Person.Api.Common.Abstractions;

namespace Zibma.Sample.Person.Api.Domain.HandlerBase
{
    public class ZibmaHandlerBase<T> where T : ApiResponseBase
    {
        private readonly ILogger _logger;
        private readonly string _handler;

        public ZibmaHandlerBase(ILogger logger, string handler)
        {
            _logger = logger;
            _handler = handler;
        }
        protected delegate Task<T> HandlerDelegate();
        protected async Task<T> TryCatch(HandlerDelegate model)
        {
            try
            {
                return await model();
            }
            catch (ZibmaException ex)
            {
                string errors = string.Join(", ", ex.Errors);
                _logger.LogInformation(ex, "{errors} at {Time} in {Handler}", errors, DateTime.Now, _handler);
                throw;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error Occur at {Time} in {Handler}", DateTime.Now, _handler);
                throw;
            }

        }

    }
}
