using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using UserManagement.Application.Exceptions;
using Mvc = Microsoft.AspNetCore.Mvc;

namespace UserManagement.WebAPI.ExceptionHandling
{
    public class ExceptionFilter : ExceptionFilterAttribute
    {
        const string ApplicationProblemJson = "application/problem+json";
        private static readonly Dictionary<Type, Func<Exception, ProblemDetails>> problemDetailsFactory = new()
        {
            { typeof(NotFoundException), _ => new ProblemDetails { Type = "https://tools.ietf.org/html/rfc7231#section-6.5.4", Status = StatusCodes.Status404NotFound } },
            { typeof(ValidationException), ex =>
                {
                    var details = new ValidationProblemDetails(((ValidationException)ex).Errors);
                    details.Type = "https://tools.ietf.org/html/rfc7231#section-6.5.1";
                    details.Status = StatusCodes.Status400BadRequest;
                    return details;
                }
            },
        };

        private readonly bool _includeDetails;

        public ExceptionFilter(IHostEnvironment environment)
        {
            _includeDetails = environment.IsDevelopment();
        }

        public override void OnException(ExceptionContext context)
        {
            context.HttpContext.Response.ContentType = ApplicationProblemJson;
            HandleException(context);
        }

        private void HandleException(ExceptionContext context)
        {
            var exception = context.Exception;
            var request = context.HttpContext.Request;

            if (problemDetailsFactory.TryGetValue(exception.GetType(), out var createDetails))
            {
                var details = createDetails(exception);
                details.Title = exception.Message;
                details.Detail = _includeDetails ? exception.ToString() : null;
                details.Instance = $"{request.Method} {request.Scheme}://{request.Host}{request.Path}{request.QueryString}";

                context.Result = new ObjectResult(details);
                context.ExceptionHandled = true;
            }
        }
    }
}
