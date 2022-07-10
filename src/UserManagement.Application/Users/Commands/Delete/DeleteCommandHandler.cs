using AutoMapper;
using MediatR;
using System.Threading;
using System.Threading.Tasks;
using UserManagement.Application.Exceptions;
using UserManagement.Application.Interfaces;
using UserManagement.Domain.Entities;

namespace UserManagement.Application.Users.Commands.Delete
{
    public record DeleteCommand(long Id) : IRequest;

    internal class DeleteCommandHandler : AsyncRequestHandler<DeleteCommand>
    {
        private readonly IUserRepository _userRepository;
        private readonly IMapper _mapper;

        public DeleteCommandHandler(IUserRepository userRepository, IMapper mapper)
        {
            _userRepository = userRepository;
            _mapper = mapper;
        }

        protected override async Task Handle(DeleteCommand command, CancellationToken cancellationToken)
        {
            var user = await _userRepository.Get(command.Id);

            if (user == null)
                throw new NotFoundException(nameof(User), command.Id);

            await _userRepository.Delete(command.Id);
        }
    }
}
