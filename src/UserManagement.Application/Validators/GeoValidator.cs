using FluentValidation;
using System.Data;
using UserManagement.Domain.Entities;

namespace UserManagement.Application.Validators
{
    class GeoValidator : AbstractValidator<Geo>
    {
        public GeoValidator()
        {
            RuleFor(g => g.Lat).NotEmpty();
            RuleFor(g => g.Lng).NotEmpty();
        }
    }
}
