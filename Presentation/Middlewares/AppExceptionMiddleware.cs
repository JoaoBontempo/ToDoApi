using Presentation.Dto.Response;
using Shared.Exceptions;
using System.Net;
using System.Text.Json;

namespace Presentation.Middlewares
{
    public class AppExceptionMiddleware
    {
        private readonly RequestDelegate _next;

        public AppExceptionMiddleware(RequestDelegate next)
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

        private static async Task HandleExceptionAsync(HttpContext context, Exception exception)
        {
            context.Response.ContentType = "application/json";

            var response = exception switch
            {
                KeyNotFoundException => CreateErrorResponse(
                    HttpStatusCode.NotFound,
                    exception.Message
                ),

                ArgumentException => CreateErrorResponse(
                    HttpStatusCode.BadRequest,
                    exception.Message
                ),

                InvalidOperationException => CreateErrorResponse(
                    HttpStatusCode.BadRequest,
                    exception.Message
                ),

                AppEntityNotFoundException => CreateErrorResponse(
                    HttpStatusCode.NotFound,
                    exception.Message
                ),

                AppInvalidDataException => CreateErrorResponse(
                    HttpStatusCode.BadRequest,
                    exception.Message
                ),

                _ => CreateErrorResponse(
                    HttpStatusCode.InternalServerError,
                    "Ocorreu um erro interno no servidor"
                )
            };

            context.Response.StatusCode = (int)response.StatusCode;
            
            var body = JsonSerializer.Serialize(response.Body);
            await context.Response.WriteAsync(body);
        }

        private static (HttpStatusCode StatusCode, AppDefaultResponse Body) CreateErrorResponse(
            HttpStatusCode statusCode,
            string errorMessage)
        {
            return (statusCode, new AppDefaultResponse(errorMessage));
        }
    }
}
