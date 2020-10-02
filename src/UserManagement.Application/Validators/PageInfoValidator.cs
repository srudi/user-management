using FluentValidation;
using UserManagement.Application.Common;

namespace UserManagement.Application.Validators
{
    class PageInfoValidator : AbstractValidator<PageInfo>
    {
        public PageInfoValidator()
        {
            RuleFor(p => p.PageSize).GreaterThan(0);
            RuleFor(p => p.PageIndex).GreaterThanOrEqualTo(0);
        }
    }
}
