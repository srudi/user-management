using FluentValidation;
using UserManagement.Domain.Entities;

namespace UserManagement.Application.Validators
{
    class CompanyValidator : AbstractValidator<Company>
    {
        public CompanyValidator()
        {
            RuleFor(C => C.Name).NotEmpty();
            RuleFor(C => C.CatchPhrase).NotEmpty();
            RuleFor(C => C.Bs).NotEmpty();
        }
    }
}
