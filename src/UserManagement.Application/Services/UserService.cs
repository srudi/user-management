using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using FluentValidation;
using UserManagement.Application.Exceptions;
using UserManagement.Application.Interfaces;
using UserManagement.Domain.Entities;

namespace UserManagement.Application.Services
{
    class UserService : IUserService
    {
        private readonly IUserRepository _userRepository;
        private readonly IValidator<User> _validator;

        public UserService(IUserRepository userRepository, IValidator<User> validator)
        {
            _userRepository = userRepository;
            _validator = validator;
        }

        public Task<IEnumerable<User>> GetAll(CancellationToken cancellationToken)
        {
            return _userRepository.GetAll(cancellationToken);
        }

        public async Task<User> Get(long id)
        {
            var user = await _userRepository.Get(id);
            if (user == null)
                throw new NotFoundException(nameof(User), id);

            return user;
        }
        public async Task Update(User user)
        {
            var validationResult = _validator.Validate(user);
            if (!validationResult.IsValid)
            {
                throw new ValidationException(validationResult.Errors);
            }

            var foundUser = await _userRepository.Get(user.Id);
            if (foundUser == null)
                throw new NotFoundException(nameof(User), user.Id);

            await _userRepository.Update(user);
        }

        public Task<long> Create(User user)
        {
            var validationResult = _validator.Validate(user);
            if (!validationResult.IsValid)
            {
                throw new ValidationException(validationResult.Errors);
            }

            return _userRepository.Create(user);
        }

        public async Task Delete(long id)
        {
            var user = await _userRepository.Get(id);
            if (user == null)
                throw new NotFoundException(nameof(User), id);

            await _userRepository.Delete(id);
        }
    }
}
