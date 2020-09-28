using MongoDB.Driver;
using Moq;
using Moq.Language.Flow;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using UserManagement.Infrastructure.Persistence.Contexts;

namespace UserManagement.Infrastructure.UnitTests.FakeDbContext
{
    static class FakeDbContextFactory
    {
        public static Mock<IMongoDbContext> Create()
        {
            return new Mock<IMongoDbContext>();
        }

        public static Mock<IMongoCollection<T>> CreateMockCollection<T>(ISetup<IMongoDbContext, IMongoCollection<T>> setup)
        {
            var mockCollection = new Mock<IMongoCollection<T>> { DefaultValue = DefaultValue.Mock };
            setup.Returns(mockCollection.Object);
            return mockCollection;
        }

        public static Mock<IMongoCollection<T>> CreateMockCollection<T>(ISetup<IMongoDbContext, IMongoCollection<T>> setup, IEnumerable<T> items)
        {
            var mockCollection = CreateMockCollection(setup);
            SetupCursor(mockCollection, items);
            return mockCollection;
        }

        private static void SetupCursor<T>(Mock<IMongoCollection<T>> collectionMock, IEnumerable<T> items)
        {
            var cursor = new FakeAsyncCursor<T>(items);
            collectionMock.Setup(c => c.FindAsync<T>(FilterDefinition<T>.Empty, null, It.IsAny<CancellationToken>())).ReturnsAsync(cursor);
            collectionMock.Setup(c => c.FindAsync<T>(It.IsAny<FilterDefinition<T>>(), null, It.IsAny<CancellationToken>())).ReturnsAsync(cursor);
            collectionMock.Setup(c => c.FindOneAndUpdateAsync(It.IsAny<FilterDefinition<T>>(), 
                                                              It.IsAny<UpdateDefinition<T>>(), 
                                                              It.IsAny<FindOneAndUpdateOptions<T, T>>(), 
                                                              It.IsAny<CancellationToken>())).ReturnsAsync(() => items.First());
        }
    }
}
