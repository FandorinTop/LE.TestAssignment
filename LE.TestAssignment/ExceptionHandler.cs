
using LE.Common.Exceptions;
using Microsoft.AspNetCore.Diagnostics;

namespace LE.WebApi
{
    public class ExceptionHandler : IExceptionHandler
    {
        private readonly ILogger<ExceptionHandler> logger;

        public ExceptionHandler(ILogger<ExceptionHandler> logger)
        {
            this.logger = logger;
        }

        public async ValueTask<bool> TryHandleAsync(
            HttpContext httpContext,
            Exception exception,
            CancellationToken cancellationToken)
        {
            var exceptionMessage = exception.Message;
            var response = new { Message = exceptionMessage };

            switch (exception)
            {
                case RecordNotFoundException notFoundException:
                    httpContext.Response.StatusCode = 400;
                    await httpContext.Response.WriteAsJsonAsync(notFoundException.Message);
                    break;
                case UnauthorizedActionException unauthorizedException:
                    httpContext.Response.StatusCode = 401;
                    await httpContext.Response.WriteAsJsonAsync(unauthorizedException.Message);
                    break;
                case ReservedFieldException reservedFieldException:
                    httpContext.Response.StatusCode = 400;
                    await httpContext.Response.WriteAsJsonAsync(reservedFieldException.Message);
                    break;
                default:
                    logger.LogError(exception.Message);
                    break;
            }

            return true;
        }
    }
}
