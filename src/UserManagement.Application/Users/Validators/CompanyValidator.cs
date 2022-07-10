using FluentValidation;
using UserManagement.Application.Users.Dtos;

namespace UserManagement.Application.Validators
{
    class CompanyValidator : AbstractValidator<CompanyDto>
    {
        public CompanyValidator()
        {
            RuleFor(C => C.Name).NotEmpty();
            RuleFor(C => C.CatchPhrase).NotEmpty();
            RuleFor(C => C.Bs).NotEmpty();
        }
    }
}
