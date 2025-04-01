using System.Net;
using System.Text.Json;
using FashionStore.BLL;
using FashionStore.BLL.Common;
using FluentValidation;
using Microsoft.AspNetCore.Diagnostics;

namespace FashionStore.PL.Middlewares
{
    public class ExceptionHandlingMiddleware : IExceptionHandler
    {
        private readonly ILogger<ExceptionHandlingMiddleware> _logger;

        public ExceptionHandlingMiddleware(ILogger<ExceptionHandlingMiddleware> logger)
        {
            _logger = logger;
        }
        public async ValueTask<bool> TryHandleAsync(HttpContext httpContext, Exception exception, CancellationToken cancellationToken)
        {

            if (exception is BusinessValidationException ex)
            {
                httpContext.Response.ContentType = "application/json";
                httpContext.Response.StatusCode = (int)HttpStatusCode.BadRequest;

                await httpContext.Response.WriteAsJsonAsync(ex.Errors.Select(e => new
                {
                    Code = e.ErrorCode,
                    Message = e.ErrorMessage
                }));     
            }
            else
            {
                _logger.LogError(exception, exception.Message);
                httpContext.Response.StatusCode = (int) HttpStatusCode.BadGateway;
            }
            return true;


        }
    }
}
