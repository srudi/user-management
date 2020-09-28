using System.Threading.Tasks;
using MongoDB.Driver;
using UserManagement.Infrastructure.Persistence.Contexts;
using UserManagement.Infrastructure.Persistence.Contexts.Models;

namespace UserManagement.Infrastructure.Persistence.Repositories
{
    class RepositoryBase
    {
        private readonly IMongoDbContext _context;

        public RepositoryBase(IMongoDbContext context)
        {
            _context = context;
        }

        public async Task<long> GetSequenceValue(string sequenceName)
        {
            var filter = Builders<Sequence>.Filter.Eq(s => s.SequenceName, sequenceName);
            var update = Builders<Sequence>.Update.Inc(s => s.SequenceValue, 1);
            var options = new FindOneAndUpdateOptions<Sequence, Sequence> { IsUpsert = true, ReturnDocument = ReturnDocument.After };
            var result = await _context.Sequences.FindOneAndUpdateAsync(filter, update, options);

            return result.SequenceValue;
        }
    }
}
