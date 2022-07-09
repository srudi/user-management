using AutoFixture;
using AutoMapper;
using FluentAssertions;
using Moq;
using System.Reflection;
using System.Threading.Tasks;
using UserManagement.Application.Exceptions;
using UserManagement.Application.Interfaces;
using UserManagement.Application.Users.Dtos;
using UserManagement.Application.Users.Queries.Get;
using UserManagement.Domain.Entities;
using Xunit;

namespace UserManagement.Application.UnitTests.Users.Queries
{
    public class GetQueryHandlerTests
    {
        private readonly Mock<IUserRepository> _repositoryMock = new Mock<IUserRepository>();
        private readonly IMapper _mapper;
        private readonly GetQueryHandler _handler;

        public GetQueryHandlerTests()
        {
            var mappingConfig = new MapperConfiguration(c => c.AddMaps(Assembly.GetAssembly(typeof(GetQueryHandler))));
            _mapper = mappingConfig.CreateMapper();
            _handler = new GetQueryHandler(_repositoryMock.Object, _mapper);
        }

        [Fact]
        public async Task Given_ValidUserId_Get_CallsTheRepositoryGetMethod()
        {
            // Arrange
            var user = new Fixture().Create<User>();
            var expectedUser = _mapper.Map<UserDto>(user);
            _repositoryMock.Setup(r => r.Get(It.IsAny<long>())).ReturnsAsync(user);

            // Act 
            var userDto = await _handler.Handle(new GetQuery(user.Id), default);

            // Assert
            _repositoryMock.Verify(r => r.Get(user.Id), Times.Once);
            userDto.Should().BeEquivalentTo(expectedUser);
        }

        [Fact]
        public async Task Given_InvalidUserId_Get_ThrowsNotFoundException()
        {
            long userId = 0;
            await Assert.ThrowsAsync<NotFoundException>(async () => await _handler.Handle(new GetQuery(userId), default));
        }
    }
}
