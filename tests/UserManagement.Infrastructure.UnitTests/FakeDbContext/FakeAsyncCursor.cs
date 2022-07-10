using MongoDB.Driver;

namespace UserManagement.Infrastructure.UnitTests.FakeDbContext
{
    public class FakeAsyncCursor<TDocument> : IAsyncCursor<TDocument>
    {
        private readonly IEnumerable<TDocument> _items;
        private readonly IEnumerator<TDocument> _enumerator;

        public FakeAsyncCursor(IEnumerable<TDocument> items)
        {
            _items = items ?? Enumerable.Empty<TDocument>();
            _enumerator = items.GetEnumerator();
        }

        public IEnumerable<TDocument> Current
        {
            get { yield return _enumerator.Current; }
        }

        public Task<bool> MoveNextAsync(CancellationToken cancellationToken)
        {
            return Task.FromResult(_enumerator.MoveNext());
        }

        public bool MoveNext(CancellationToken cancellationToken = default)
        {
            var moveNext = _enumerator.MoveNext();
            return moveNext;
        }
        public void Dispose() { }
    }
}
