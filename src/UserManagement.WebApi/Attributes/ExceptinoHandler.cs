using Microsoft.AspNetCore.Mvc;
using System.Text.Json;

namespace UserManagement.WebAPI.Attributes
{
    /// <summary>
    /// For handling unknown, unexpected exceptions using the problem details standard.
    /// </summary>
    internal class ExceptinoHandler : IMiddleware
    {
        const string ApplicationProblemJson = "application/problem+json";
        const string DefaultErrorMessage = "An error occurred while processing your request.";
        private static bool _includeDetails;
        private static ILogger _logger;

        public ExceptinoHandler(ILogger<ExceptinoHandler> logger, IHostEnvironment environment)
        {
            _logger = logger;
            _includeDetails = environment.IsDevelopment();
        }
        public async Task InvokeAsync(HttpContext context, RequestDelegate next)
        {
            try
            {
                await next(context);
            }
            catch (Exception exception)
            {
                LogError(context, exception);
                await WriteResponse(context, exception);
            }
        }

        private void LogError(HttpContext httpContext, Exception exception)
        {
            var problemDetailsToLog = CreateProblemDetails(httpContext, includeDetails: true, exception);
            _logger.LogError("Error: {@ProblemDetails}", problemDetailsToLog);
        }

        private static ProblemDetails CreateProblemDetails(HttpContext httpContext, bool includeDetails, Exception exception)
        {
            var request = httpContext.Request;

            return new ProblemDetails
            {
                Type = "https://tools.ietf.org/html/rfc7231#section-6.6.1",
                Status = StatusCodes.Status500InternalServerError,
                Title = includeDetails ? exception.Message : DefaultErrorMessage,
                Detail = includeDetails ? exception.ToString() : null,
                Instance = $"{request.Method} {request.Scheme}://{request.Host}{request.Path}{request.QueryString}"
            };
        }

        private static Task WriteResponse(HttpContext context, Exception exception)
        {
            var problemDetailsToReply = CreateProblemDetails(context, _includeDetails, exception);
            context.Response.ContentType = ApplicationProblemJson;
            var stream = context.Response.Body;
            return JsonSerializer.SerializeAsync(stream, problemDetailsToReply);
        }
    }
}
