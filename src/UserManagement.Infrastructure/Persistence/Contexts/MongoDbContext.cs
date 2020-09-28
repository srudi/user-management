using Microsoft.Extensions.Options;
using MongoDB.Driver;
using UserManagement.Infrastructure.Configuration;
using UserManagement.Infrastructure.Persistence.Contexts.Models;

namespace UserManagement.Infrastructure.Persistence.Contexts
{
    class MongoDbContext : IMongoDbContext
    {
        private readonly IMongoDatabase _db;
        public MongoDbContext(IOptions<MongoDBConfig> configOption)
        {
            var client = new MongoClient(configOption.Value.ConnectionString);
            _db = client.GetDatabase(configOption.Value.Database);
        }

        public IMongoCollection<User> Users => _db.GetCollection<User>(nameof(Users));

        public IMongoCollection<Sequence> Sequences => _db.GetCollection<Sequence>(nameof(Sequences));
    }
}
