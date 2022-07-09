using MediatR;
using UserManagement.Application.Common;

namespace UserManagement.Application.Users.Queries
{
    public class GetAllPagedQuery : IRequest<PagedResult<UserDto>>
    {
        public PageInfo PageInfo { get; set; }
    }
}
