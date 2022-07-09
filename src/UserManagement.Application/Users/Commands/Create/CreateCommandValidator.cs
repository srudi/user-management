using FluentValidation;
using UserManagement.Application.Users.Commands.Update;
using UserManagement.Application.Users.Dtos;

namespace UserManagement.Application.Users.Commands.Create
{
    internal class CreateCommandValidator : AbstractValidator<UpdateCommand>
    {
        public CreateCommandValidator(IValidator<UserDto> userValidator)
        {
            RuleFor(command => command.User).SetValidator(userValidator);
        }
    }
}
