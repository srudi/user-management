using FluentValidation;
using UserManagement.Application.Users.Dtos;

namespace UserManagement.Application.Validators
{
    class GeoValidator : AbstractValidator<GeoDto>
    {
        public GeoValidator()
        {
            RuleFor(g => g.Lat).NotEmpty();
            RuleFor(g => g.Lng).NotEmpty();
        }
    }
}
