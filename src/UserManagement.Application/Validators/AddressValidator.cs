using FluentValidation;
using UserManagement.Domain.ValueObjects;

namespace UserManagement.Application.Validators
{
    class AddressValidator : AbstractValidator<Address>
    {
        public AddressValidator()
        {
            RuleFor(a => a.Street).NotEmpty();
            RuleFor(a => a.Suite).NotEmpty();
            RuleFor(a => a.City).NotEmpty();
            RuleFor(a => a.Zipcode).NotEmpty();
            RuleFor(a => a.Geo).NotEmpty().SetValidator(new GeoValidator());
        }
    }
}
