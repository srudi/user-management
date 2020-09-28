using FluentValidation;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using System.Threading.Tasks;
using UserManagement.Application.Exceptions;

namespace UserManagement.WebAPI.Attributes
{
    public class GeneralExceptionFilterAttribute : ExceptionFilterAttribute
    {
        public override Task OnExceptionAsync(ExceptionContext context)
        {
            switch (context.Exception)
            {
                case ValidationException validationException:
                    context.Result = new BadRequestObjectResult(validationException.Errors);
                    break;
                case NotFoundException notFoundException:
                    context.Result = new NotFoundObjectResult(notFoundException.Message);
                    break;
            }
            
            return Task.CompletedTask;
        }
    }

}
