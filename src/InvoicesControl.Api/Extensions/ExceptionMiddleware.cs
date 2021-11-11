using Microsoft.AspNetCore.Http;
using System.Net;
using System.Threading.Tasks;

namespace InvoicesControl.Api.Extensions
{
    public class ExceptionMiddleware
    {
        private readonly RequestDelegate _next;

        public ExceptionMiddleware(RequestDelegate next)
        {
            _next = next;
        }

        public async Task InvokeAsync(HttpContext httpContext)
        {
            try
            {
                await _next(httpContext);
            }
            catch
            {
                HandleExceptionAsync(httpContext);
            }
        }

        private static void HandleExceptionAsync(HttpContext context)
        {
            context.Response.StatusCode = (int)HttpStatusCode.InternalServerError;
        }
    }
}
