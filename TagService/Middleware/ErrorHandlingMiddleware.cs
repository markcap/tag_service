using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Hosting;
using Serilog;
using System;
using System.Net;
using System.Text.Json;
using System.Threading.Tasks;
using TagService.Exceptions;

namespace TagService.Middleware
{
    public class ErrorHandlingMiddleware
    {
        private readonly RequestDelegate _next;
        static readonly ILogger _logger = Serilog.Log.ForContext<ErrorHandlingMiddleware>();

        public ErrorHandlingMiddleware(RequestDelegate next)
        {
            _next = next;
        }

        public async Task InvokeAsync(HttpContext context, IWebHostEnvironment env)
        {
            try
            {
                await _next(context);
            }
            catch (Exception ex)
            {
                await HandleExceptionAsync(context, ex, env);
            }
        }

        private static Task HandleExceptionAsync(HttpContext context, Exception exception, IWebHostEnvironment env)
        {
            HttpStatusCode status;

            var errorResponseData = new ErrorResponseData()
            {
                Message = exception.Message,
                StackTrace = (!env.IsProduction()) ? exception.StackTrace : string.Empty
            };

            switch (exception)
            {
                case UexpressNotFoundException _:
                    status = HttpStatusCode.NotFound;
                    break;
                case ArgumentException _:
                    status = HttpStatusCode.BadRequest;
                    break;
                default:
                    status = HttpStatusCode.InternalServerError;
                    break;
            }

            errorResponseData.Status = status;

            var result = JsonSerializer.Serialize(errorResponseData);
            context.Response.ContentType = "application/json";
            context.Response.StatusCode = (int)status;
            return context.Response.WriteAsync(result);
        }
    }
}
