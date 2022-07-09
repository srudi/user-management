using FluentValidation;
using UserManagement.Application.Common;

namespace UserManagement.Application.Users.Queries.GetAll
{
    public class GetAllPagedQueryValidator : AbstractValidator<GetAllPagedQuery>
    {
        public GetAllPagedQueryValidator(IValidator<PageInfo> pageInfoValidator)
        {
            RuleFor(query => query.PageInfo).SetValidator(pageInfoValidator);
        }
    }
}
