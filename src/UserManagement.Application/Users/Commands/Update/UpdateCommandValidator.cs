using FluentValidation;
using UserManagement.Application.Users.Dtos;

namespace UserManagement.Application.Users.Commands.Update
{
    class UpdateCommandValidator : AbstractValidator<UpdateCommand>
    {
        public UpdateCommandValidator(IValidator<UserDto> userValidator)
        {
            RuleFor(command => command.User).SetValidator(userValidator);
        }
    }
}
