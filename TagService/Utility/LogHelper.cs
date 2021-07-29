using Microsoft.AspNetCore.Http;
using Serilog;
using Serilog.Events;
using System;

namespace TagService.Utility
{
    public class LogHelper
    {
        /// <summary>
        /// Microsoft logging is too verbose, so we mute it and use Serilog request logging middleware instead.
        /// This helper lets us add properties that are missing out of the box.
        /// </summary>
        /// <remarks>
        /// https://andrewlock.net/using-serilog-aspnetcore-in-asp-net-core-3-logging-the-selected-endpoint-name-with-serilog/
        /// </remarks>
        /// <param name="diagnosticContext"></param>
        /// <param name="httpContext"></param>

        public static void EnrichFromRequest(IDiagnosticContext diagnosticContext, HttpContext httpContext)
        {
            var request = httpContext.Request;

            // Set all the common properties available for every request
            diagnosticContext.Set("Host", request.Host);

            // Only set it if available. You're not sending sensitive data in a querystring right?!
            if (request.QueryString.HasValue)
            {
                diagnosticContext.Set("QueryString", request.QueryString.Value);
            }
        }

        /// <summary>
        /// Serilog helper to exclude health check endpoints from request logging
        /// </summary>
        /// <remarks>
        /// https://andrewlock.net/using-serilog-aspnetcore-in-asp-net-core-3-excluding-health-check-endpoints-from-serilog-request-logging/
        /// </remarks>
        public static LogEventLevel ExcludeHealthChecks(HttpContext ctx, double _, Exception ex) =>
            ex != null
                ? LogEventLevel.Error
                : ctx.Response.StatusCode > 499
                    ? LogEventLevel.Error
                    : IsHealthCheckEndpoint(ctx) // Not an error, check if it was a health check
                        ? LogEventLevel.Verbose // Was a health check, use Verbose
                        : LogEventLevel.Information;

        private static bool IsHealthCheckEndpoint(HttpContext ctx)
        {
            var endpoint = ctx.GetEndpoint();
            if (endpoint is object) // same as !(endpoint is null)
            {
                return string.Equals(
                    endpoint.DisplayName,
                    "Health checks",
                    StringComparison.Ordinal);
            }
            // No endpoint, so not a health check endpoint
            return false;
        }
    }
}
