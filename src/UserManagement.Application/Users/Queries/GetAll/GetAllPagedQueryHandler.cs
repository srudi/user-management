using AutoMapper;
using MediatR;
using UserManagement.Application.Common;
using UserManagement.Application.Interfaces;
using UserManagement.Application.Users.Dtos;

namespace UserManagement.Application.Users.Queries.GetAll
{
    public record GetAllPagedQuery(PageInfo PageInfo) : IRequest<PagedResult<UserDto>>;

    internal class GetAllPagedQueryHandler : IRequestHandler<GetAllPagedQuery, PagedResult<UserDto>>
    {
        private readonly IUserRepository _userRepository;
        private readonly IMapper _mapper;

        public GetAllPagedQueryHandler(IUserRepository userRepository, IMapper mapper)
        {
            _userRepository = userRepository;
            _mapper = mapper;
        }

        public async Task<PagedResult<UserDto>> Handle(GetAllPagedQuery request, CancellationToken cancellationToken)
        {
            var pageInfo = request.PageInfo;
            var domainUsers = await _userRepository.GetAll(pageInfo, cancellationToken);
            pageInfo.TotalCount = await _userRepository.GetTotalCount(cancellationToken);
            var users = _mapper.Map<IEnumerable<UserDto>>(domainUsers);
            return new PagedResult<UserDto>(users, pageInfo);
        }
    }
}
