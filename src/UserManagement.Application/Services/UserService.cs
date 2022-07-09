using System.Threading;
using System.Threading.Tasks;
using FluentValidation;
using UserManagement.Application.Common;
using UserManagement.Application.Exceptions;
using UserManagement.Application.Interfaces;
using UserManagement.Domain.Entities;

namespace UserManagement.Application.Services
{
    class UserService : IUserService
    {
        private readonly IUserRepository _userRepository;
        private readonly IValidator<User> _userValidator;
        private readonly IValidator<PageInfo> _pageInfoValidator;

        public UserService(IUserRepository userRepository, IValidator<User> userValidator, IValidator<PageInfo> pageInfoValidator)
        {
            _userRepository = userRepository;
            _pageInfoValidator = pageInfoValidator;
            _userValidator = userValidator;
        }

        //public Task<PagedResult<User>> GetAll(PageInfo pageInfo, CancellationToken cancellationToken)
        //{
        //   var validationResult = _pageInfoValidator.Validate(pageInfo);
        //    if (!validationResult.IsValid)
        //    {
        //        throw new ValidationException(validationResult.Errors);
        //    }

        //    return _userRepository.GetAll(pageInfo, cancellationToken);
        //}

        public async Task<User> Get(long id)
        {
            var user = await _userRepository.Get(id);
            if (user == null)
                throw new NotFoundException(nameof(User), id);

            return user;
        }
        public async Task Update(User user)
        {
            var validationResult = _userValidator.Validate(user);
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
            var validationResult = _userValidator.Validate(user);
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
