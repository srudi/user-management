using AutoMapper;
using MediatR;
using System.Threading;
using System.Threading.Tasks;
using UserManagement.Application.Exceptions;
using UserManagement.Application.Interfaces;
using UserManagement.Application.Users.Dtos;
using UserManagement.Domain.Entities;

namespace UserManagement.Application.Users.Queries.Get
{
    public record GetQuery(long Id) : IRequest<UserDto>;

    internal class GetQueryHandler : IRequestHandler<GetQuery, UserDto>
    {
        private readonly IUserRepository _userRepository;
        private readonly IMapper _mapper;

        public GetQueryHandler(IUserRepository userRepository, IMapper mapper)
        {
            _userRepository = userRepository;
            _mapper = mapper;
        }

        public async Task<UserDto> Handle(GetQuery request, CancellationToken cancellationToken)
        {
            var domainUser = await _userRepository.Get(request.Id);
            if (domainUser == null)
                throw new NotFoundException(nameof(User), request.Id);

            return _mapper.Map<UserDto>(domainUser);
        }
    }
}
