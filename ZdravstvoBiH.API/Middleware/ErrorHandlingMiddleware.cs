using System.Net;
using System.Text.Json;

namespace ZdravstvoBiH.API.Middleware
{
    public class ErrorHandlingMiddleware
    {
        private readonly RequestDelegate _next;

        public ErrorHandlingMiddleware(RequestDelegate next)
        {
            _next = next;   
        }

        public async Task InvokeAsync(HttpContext context)
        {
            try
            {
                await _next(context);
            }
            catch(Exception ex)
            {
                await HandleExeptionAsync(context, ex);
            }
        }

        private  Task HandleExeptionAsync(HttpContext context, Exception ex)
        {
            context.Response.ContentType = "application/json";
            context.Response.StatusCode = (int)HttpStatusCode.BadRequest;

            var response = new
            {
                success = false,
                message = ex.Message,
                data = (object?)null
            };

            var json = JsonSerializer.Serialize(response);  
            return context.Response.WriteAsync(json);

        }
    }
}
