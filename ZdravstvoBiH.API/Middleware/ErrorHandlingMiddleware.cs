using System.Net;
using System.Text.Json;
using Microsoft.Extensions.Hosting;

namespace ZdravstvoBiH.API.Middleware
{
    public class ErrorHandlingMiddleware
    {
        private readonly RequestDelegate _next;
        private readonly IHostEnvironment _env;

        public ErrorHandlingMiddleware(RequestDelegate next, IHostEnvironment env)
        {
            _next = next;
            _env = env;
        }

        public async Task InvokeAsync(HttpContext context)
        {
            try
            {
                await _next(context);
            }
            catch(Exception ex)
            {
                await HandleExceptionAsync(context, ex);
            }
        }

        private Task HandleExceptionAsync(HttpContext context, Exception ex)
        {
            context.Response.ContentType = "application/json";

            var response = new
            {
                success = false,
                message = ex.Message,
                details = _env.IsDevelopment() ? ex.StackTrace : null
            };

            // Default to 500
            var statusCode = HttpStatusCode.InternalServerError;

            switch (ex)
            {
                case UnauthorizedAccessException:
                    statusCode = HttpStatusCode.Forbidden; // 403
                    break;
                case ArgumentException:
                    statusCode = HttpStatusCode.BadRequest; // 400
                    break;
                case KeyNotFoundException:
                    statusCode = HttpStatusCode.NotFound; // 404
                    break;
                case InvalidOperationException:
                    statusCode = HttpStatusCode.Conflict; // 409 - business rule violation
                    break;
                case Microsoft.EntityFrameworkCore.DbUpdateException:
                    statusCode = HttpStatusCode.Conflict; // 409 - DB update / unique constraint
                    break;
                default:
                    statusCode = HttpStatusCode.InternalServerError;
                    break;
            }

            context.Response.StatusCode = (int)statusCode;

            var json = JsonSerializer.Serialize(response);
            return context.Response.WriteAsync(json);
        }
    }
}
