using System.Linq.Expressions;
using ANDON_Domain.Exceptions;

namespace ANDON.Middleware
{
    public class ExceptionMiddleware
    {
        private readonly RequestDelegate _next;
        public ExceptionMiddleware(RequestDelegate next)
        {
            _next = next;
        }
        public async Task InvokeAsync(HttpContext context)
        {
            try
            {
                await _next(context);
            }
            catch (Exception ex)
            {
                await HandleExceptionAsync(context, ex);
            }
        }
        private async Task HandleExceptionAsync(HttpContext context, Exception ex)
        {
            context.Response.ContentType = "application/json";
            switch (ex)
            {
                case NotFoundException:
                    await context.Response.WriteAsync(new BaseException()
                    {
                        ErrorCode = ((NotFoundException)ex).ErrorCode,
                        DevMessage = ((NotFoundException)ex).DevMessage,
                        UserMessage = ((NotFoundException)ex).UserMessage,
                        Errors = { }

                    }.ToString() ?? "");
                    break;
                case BadRequestException:
                    context.Response.StatusCode = StatusCodes.Status400BadRequest;
                    await context.Response.WriteAsync(new BaseException()
                    {
                        ErrorCode = ((BadRequestException)ex).ErrorCode,
                        DevMessage = ((BadRequestException)ex).DevMessage,
                        UserMessage = ((BadRequestException)ex).UserMessage,
                        Errors = { }

                    }.ToString() ?? "");
                    break;
                case UnauthoriedException:
                    context.Response.StatusCode = StatusCodes.Status401Unauthorized;
                    await context.Response.WriteAsync(new BaseException()
                    {
                        ErrorCode = ((UnauthoriedException)ex).ErrorCode,
                        DevMessage = ((UnauthoriedException)ex).DevMessage,
                        UserMessage = ((UnauthoriedException)ex).UserMessage,
                        Errors = { }

                    }.ToString() ?? "");
                    break;
                default:
                    context.Response.StatusCode = StatusCodes.Status500InternalServerError;
                    await context.Response.WriteAsync(new BaseException()
                    {
                        ErrorCode = ((ServerInternalErrorException)ex).ErrorCode,
                        DevMessage = ((ServerInternalErrorException)ex).DevMessage,
                        UserMessage = ((ServerInternalErrorException)ex).UserMessage,
                        Errors = { }

                    }.ToString() ?? "");
                    break;
            }
        }
    }
}
