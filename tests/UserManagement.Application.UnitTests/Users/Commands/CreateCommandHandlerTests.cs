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
        public async Task Given_ValidUser_When_CreateCalled_Then_CallsTheRepositoryCreateMethodAndReturnsTheCreatedUser()
        {
            // Arrange
            var user = new Fixture().Create<UserDto>();
            var domainUser = _mapper.Map<User>(user);

            var command = new CreateCommand(user);
            _repositoryMock.Setup(r => r.Create(It.IsAny<User>())).ReturnsAsync(It.IsAny<long>());
            _repositoryMock.Setup(r => r.Get(It.IsAny<long>())).ReturnsAsync(domainUser);

            // Act 
            var createdUser = await _handler.Handle(command, default);

            // Assert
            _repositoryMock.Verify(r => r.Create(It.IsAny<User>()), Times.Once);
            createdUser.Should().BeEquivalentTo(user);
        }
    }
}
