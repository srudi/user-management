using AutoMapper;
using MediatR;
using System.Threading;
using System.Threading.Tasks;
using UserManagement.Application.Interfaces;

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

        protected override Task Handle(DeleteCommand command, CancellationToken cancellationToken)
        {
            return _userRepository.Delete(command.Id);
        }
    }
}
