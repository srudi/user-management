using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using MongoDB.Driver;
using Moq;
using UserManagement.Application.Common;
using UserManagement.Infrastructure.Persistence.Contexts;
using UserManagement.Infrastructure.Persistence.Contexts.Models;
using UserManagement.Infrastructure.Persistence.Exceptions;
using UserManagement.Infrastructure.Persistence.Repositories;
using UserManagement.Infrastructure.UnitTests.FakeDbContext;
using Xunit;
using DomainUser = UserManagement.Domain.Entities.User;

namespace UserManagement.Infrastructure.UnitTests.Persistence
{
    [Collection("RepositoryTests")]
    public class UserRepositoryTests
    {
        private readonly Mock<IMongoDbContext> _dbContextMock;
        private readonly UserRepository _userRepository;

        public UserRepositoryTests(RepositoryTestFixture fixture)
        {
            _dbContextMock = FakeDbContextFactory.Create();
            _userRepository = new UserRepository(_dbContextMock.Object, fixture.Mapper);
        }

        [Fact]
        public async Task When_CreateCalled_Then_ShouldCallCreateOnDbContext()
        {
            // Arrange
            var user = new DomainUser();
            var autoIncrementedUserId = 1;
            FakeDbContextFactory.CreateMockCollection(_dbContextMock.Setup(db => db.Users));
            FakeDbContextFactory.CreateMockCollection(_dbContextMock.Setup(db => db.Sequences), new[] { new Sequence { SequenceValue = 1 } });

            // Act
            var userId = await _userRepository.Create(user);

            // Assert
            _dbContextMock.Verify(db => db.Users.InsertOneAsync(It.IsAny<User>(), It.IsAny<InsertOneOptions>(), default), Times.Once);
            Assert.Equal(autoIncrementedUserId, userId);
        }

        [Fact]
        public async Task When_DeleteCalledSuccessfully_Then_ShouldNotThrowException()
        {
            // Arrange
            var userIdToDelete = 1;
            var resultMock = new Mock<DeleteResult>();
            resultMock.Setup(r => r.IsAcknowledged).Returns(true);
            resultMock.Setup(r => r.DeletedCount).Returns(1);
            _dbContextMock.Setup(db => db.Users.DeleteOneAsync(It.IsAny<FilterDefinition<User>>(), default)).ReturnsAsync(resultMock.Object);

            // Act & Assert
            await _userRepository.Delete(userIdToDelete);
        }

        [Theory]
        [InlineData(false, 1)]
        [InlineData(true, 0)]
        [InlineData(false, 0)]
        public async Task When_DeleteCalledDoesNotIndicateSuccess_Then_ThrowsException(bool isAcknowledged, long deletedCount)
        {
            // Arrange
            var userIdToDelete = 1;
            var resultMock = new Mock<DeleteResult>();
            resultMock.Setup(r => r.IsAcknowledged).Returns(isAcknowledged);
            resultMock.Setup(r => r.DeletedCount).Returns(deletedCount);
            _dbContextMock.Setup(db => db.Users.DeleteOneAsync(It.IsAny<FilterDefinition<User>>(), default)).ReturnsAsync(resultMock.Object);

            // Act & Assert
            await Assert.ThrowsAsync<DatabaseUpdateException>(async () => await _userRepository.Delete(userIdToDelete));
        }

        [Fact]
        public async Task When_UpdateCalledSuccessfully_Then_ShouldNotThrowException()
        {
            // Arrange
            var user = new DomainUser();
            var resultMock = new Mock<ReplaceOneResult>();
            resultMock.Setup(r => r.IsAcknowledged).Returns(true);
            resultMock.Setup(r => r.ModifiedCount).Returns(1);
            _dbContextMock.Setup(db => db.Users.ReplaceOneAsync(It.IsAny<FilterDefinition<User>>(), It.IsAny<User>(), It.IsAny<ReplaceOptions>(), default)).ReturnsAsync(resultMock.Object);

            // Act & Assert
            await _userRepository.Update(user);
        }

        [Theory]
        [InlineData(false, 1)]
        [InlineData(true, 0)]
        [InlineData(false, 0)]
        public async Task When_UpdateCalledReplaceOneResultDoesNotIndicateSuccess_Then_ThrowsException(bool isAcknowledged, long modifiedCount)
        {
            // Arrange
            var user = new DomainUser();
            var resultMock = new Mock<ReplaceOneResult>();
            resultMock.Setup(r => r.IsAcknowledged).Returns(isAcknowledged);
            resultMock.Setup(r => r.ModifiedCount).Returns(modifiedCount);
            _dbContextMock.Setup(db => db.Users.ReplaceOneAsync(It.IsAny<FilterDefinition<User>>(), It.IsAny<User>(), It.IsAny<ReplaceOptions>(), default)).ReturnsAsync(resultMock.Object);

            // Act & Assert
            await Assert.ThrowsAsync<DatabaseUpdateException>(async () => await _userRepository.Update(user));
        }

        [Theory]
        [InlineData(2,0,2)]
        [InlineData(5,0,5)]
        [InlineData(5,1,0)]
        [InlineData(2,2,1)]
        public async Task When_GetAllCalled_Then_ShouldCallFindAsyncOnDbContext_And_ReturnPagedUsersList(int pageSize, int pageIndex, int expcetedPagedUserCount)
        {
            // Arrange
            var pageInfo = new PageInfo(pageSize, pageIndex);
            var users = new List<User>
            {
                new User {  Id = 1, Name = "John" },
                new User {  Id = 2, Name = "John" },
                new User {  Id = 3, Name = "John" },
                new User {  Id = 4, Name = "John" },
                new User {  Id = 5, Name = "John" }
            };

            var findOptions = new FindOptions<User, User> { Skip = pageInfo.CalculateSkip(), Limit = pageInfo.PageSize };
            FakeDbContextFactory.CreateMockCollection(_dbContextMock.Setup(db => db.Users), users, findOptions);

            // Act
            var pagedUsers = await _userRepository.GetAll(pageInfo, CancellationToken.None);

            // Assert
            Assert.Equal(expcetedPagedUserCount, pagedUsers.Count());
            _dbContextMock.Verify(db => db.Users.FindAsync(FilterDefinition<User>.Empty, It.IsAny<FindOptions<User, User>>(), It.IsAny<CancellationToken>()), Times.Once);
        }

        [Fact]
        public async Task Call_Get_ShouldCall_FindAsyncOnDbContext_And_ReturnAUser()
        {
            // Arrange
            var userId = 1;

            var users = new List<User>
            {
                new User {  Id = userId, Name = "John" },
            };

            FakeDbContextFactory.CreateMockCollection(_dbContextMock.Setup(db => db.Users), users);

            // Act
            var user = await _userRepository.Get(userId);

            // Assert
            Assert.NotNull(user);
            _dbContextMock.Verify(db => db.Users.FindAsync<User>(It.IsAny<FilterDefinition<User>>(), null, default), Times.Once);
        }
    }
}
