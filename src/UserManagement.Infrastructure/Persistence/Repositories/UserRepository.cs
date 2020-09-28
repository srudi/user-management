using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using AutoMapper;
using MongoDB.Driver;
using UserManagement.Application.Interfaces;
using UserManagement.Infrastructure.Persistence.Contexts;
using UserManagement.Infrastructure.Persistence.Contexts.Models;
using UserManagement.Infrastructure.Persistence.Exceptions;
using DomainUser = UserManagement.Domain.Entities.User;

namespace UserManagement.Infrastructure.Persistence.Repositories
{
    class UserRepository : RepositoryBase, IUserRepository
    {
        private readonly IMongoDbContext _context;
        private readonly IMapper _mapper;

        public UserRepository(IMongoDbContext context,  IMapper mapper) : base(context)
        {
            _context = context;
            _mapper = mapper;
        }

        public async Task<long> Create(DomainUser domainUser)
        {
            var user = _mapper.Map<User>(domainUser);
            user.Id = await GetSequenceValue(nameof(_context.Users));
            await _context.Users.InsertOneAsync(user);
            return user.Id;
        }

        public async Task Delete(long id)
        {
            var deleteResult = await _context.Users.DeleteOneAsync(u => u.Id == id);
            var successfullyDeleted = deleteResult.IsAcknowledged && deleteResult.DeletedCount > 0;
            if (!successfullyDeleted)
            {
                throw new DatabaseUpdateException();
            }
        }

        public async Task<DomainUser> Get(long id)
        {
            var user = (await _context.Users.FindAsync(u => u.Id == id)).FirstOrDefault();
            return _mapper.Map<DomainUser>(user);
        }

        public async Task<IEnumerable<DomainUser>> GetAll(CancellationToken cancellationToken)
        {
            // Hm, paging maybe???
            var users = (await _context.Users.FindAsync(FilterDefinition<User>.Empty, null, cancellationToken)).ToList();
            return _mapper.Map<IEnumerable<DomainUser>>(users);
        }

        public async Task Update(DomainUser domainUser)
        {
            var user = _mapper.Map<User>(domainUser);
            var updateResult = await _context.Users.ReplaceOneAsync(u => u.Id == user.Id, user);
            var successfullyUpdated = updateResult.IsAcknowledged && updateResult.ModifiedCount > 0;
            if (!successfullyUpdated)
            {
                throw new DatabaseUpdateException();
            }
        }
    }
}
