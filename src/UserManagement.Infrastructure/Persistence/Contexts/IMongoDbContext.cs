using MongoDB.Driver;
using UserManagement.Infrastructure.Persistence.Contexts.Models;

namespace UserManagement.Infrastructure.Persistence.Contexts
{
    public interface IMongoDbContext
    {
        public IMongoCollection<User> Users { get; }

        public IMongoCollection<Sequence> Sequences { get; }
    }
}
