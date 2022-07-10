using AutoFixture;
using AutoMapper;
using FluentAssertions;
using Moq;
using System.Reflection;
using System.Threading.Tasks;
using UserManagement.Application.Interfaces;
using UserManagement.Application.Users.Commands.Create;
using UserManagement.Application.Users.Dtos;
using UserManagement.Domain.Entities;
using Xunit;

namespace UserManagement.Application.UnitTests.Users.Commands
{
    public class CreateCommandHandlerTests
    {
        private readonly Mock<IUserRepository> _repositoryMock = new Mock<IUserRepository>();
        private readonly IMapper _mapper;
        private readonly CreateCommandHandler _handler;

        public CreateCommandHandlerTests()
        {
            var mappingConfig = new MapperConfiguration(c => c.AddMaps(Assembly.GetAssembly(typeof(CreateCommandHandler))));
            _mapper = mappingConfig.CreateMapper();
            _handler = new CreateCommandHandler(_repositoryMock.Object, _mapper);
        }


        [Fact]
        public async Task Given_ValidUser_Create_CallsTheRepositoryCreateMethod()
        {
            // Arrange
            var expectedUserId = 100;
            var user = new Fixture().Create<UserDto>();
            var command = new CreateCommand(user);
            _repositoryMock.Setup(r => r.Create(It.IsAny<User>())).ReturnsAsync(expectedUserId);
            // Act 
            var userId = await _handler.Handle(command, default);

            // Assert
            _repositoryMock.Verify(r => r.Create(It.IsAny<User>()), Times.Once);
            userId.Should().Be(expectedUserId);
        }
    }
}
