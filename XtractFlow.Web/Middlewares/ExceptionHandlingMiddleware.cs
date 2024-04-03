using System.Net.Mime;

namespace XtractFlow.Web.Middlewares
{
    public class ExceptionHandlingMiddleware
    {
        private readonly RequestDelegate _next;

        public ExceptionHandlingMiddleware(RequestDelegate next)
        {
            _next = next;
        }

        public async Task InvokeAsync(HttpContext context)
        {
            try
            {
                await _next.Invoke(context);
            }
            catch (Exception ex)
            {
                await HandleExceptionAsync(context, ex);
            }
        }

        private async Task HandleExceptionAsync(HttpContext context, Exception exception)
        {
            string errorMessage = exception.Message;

            var response = context.Response;
            response.ContentType = MediaTypeNames.Application.Json;
            await response.WriteAsync("An unhandled exception has occurred: " + exception.Message);
        }
    }
}