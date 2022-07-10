using FluentValidation;
using UserManagement.Application.Users.Dtos;

namespace UserManagement.Application.Validators
{
    class AddressValidator : AbstractValidator<AddressDto>
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
