using AutoMapper;
using MediatR;
using System.Threading;
using System.Threading.Tasks;
using UserManagement.Application.Interfaces;
using UserManagement.Application.Users.Dtos;
using UserManagement.Domain.Entities;

namespace UserManagement.Application.Users.Commands.Create
{
    public record CreateCommand(UserDto User) : IRequest<UserDto>;

    internal class CreateCommandHandler : IRequestHandler<CreateCommand, UserDto>
    {
        private readonly IUserRepository _userRepository;
        private readonly IMapper _mapper;

        public CreateCommandHandler(IUserRepository userRepository, IMapper mapper)
        {
            _userRepository = userRepository;
            _mapper = mapper;
        }

        public async Task<UserDto> Handle(CreateCommand command, CancellationToken cancellationToken)
        {
            var domainUser = _mapper.Map<User>(command.User);
            var createduserId = await _userRepository.Create(domainUser);
            var createdUser = await _userRepository.Get(createduserId);
            return _mapper.Map<UserDto>(createdUser);
        }
    }
}
