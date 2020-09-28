using FluentValidation;
using UserManagement.Domain.Entities;

namespace UserManagement.Application.Validators
{
    class UserValidator : AbstractValidator<User>
    {
        public UserValidator()
        {
            RuleFor(u => u.Name).NotEmpty();
            RuleFor(u => u.Username).NotEmpty();
            RuleFor(u => u.Email).NotEmpty().EmailAddress();
            RuleFor(u => u.Address).NotNull().SetValidator(new AddressValidator());
            RuleFor(u => u.Phone).NotEmpty();
            RuleFor(u => u.Website).NotEmpty();
            RuleFor(u => u.Company).NotNull().SetValidator(new CompanyValidator());
        }
    }
}
