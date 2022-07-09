using FluentValidation;

namespace UserManagement.Application.Common
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
