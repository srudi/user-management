using AutoMapper;
using MediatR;
using UserManagement.Application.Exceptions;
using UserManagement.Application.Interfaces;
using UserManagement.Application.Users.Dtos;
using UserManagement.Domain.Entities;

namespace UserManagement.Application.Users.Commands.Update
{
    public record UpdateCommand(UserDto User) : IRequest;

    internal class UpdateCommandHandler : AsyncRequestHandler<UpdateCommand>
    {
        private readonly IUserRepository _userRepository;
        private readonly IMapper _mapper;

        public UpdateCommandHandler(IUserRepository userRepository, IMapper mapper)
        {
            _userRepository = userRepository;
            _mapper = mapper;
        }

        protected override async Task Handle(UpdateCommand command, CancellationToken cancellationToken)
        {
            var user = command.User;
            var foundUser = await _userRepository.Get(user.Id);
            if (foundUser == null)
                throw new NotFoundException(nameof(User), user.Id);

            var domainUser = _mapper.Map<User>(user);
            await _userRepository.Update(domainUser);
        }
    }
}
