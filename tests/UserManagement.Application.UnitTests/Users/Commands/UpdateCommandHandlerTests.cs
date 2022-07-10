using AutoFixture;
using AutoMapper;
using Moq;
using System.Reflection;
using UserManagement.Application.Exceptions;
using UserManagement.Application.Interfaces;
using UserManagement.Application.Users.Commands.Update;
using UserManagement.Application.Users.Dtos;
using UserManagement.Domain.Entities;
using Xunit;

namespace UserManagement.Application.UnitTests.Users.Commands
{
    public  class UpdateCommandHandlerTests
    {
        private readonly Mock<IUserRepository> _repositoryMock = new Mock<IUserRepository>();
        private readonly IMapper _mapper;
        private readonly TestHandler _handler;

        public UpdateCommandHandlerTests()
        {
            var mappingConfig = new MapperConfiguration(c => c.AddMaps(Assembly.GetAssembly(typeof(UpdateCommandHandler))));
            _mapper = mappingConfig.CreateMapper();
            _handler = new TestHandler(_repositoryMock.Object, _mapper);
        }

        [Fact]
        public async Task Given_ValidExistingUser_When_UpdateCallaed_Then_CallsTheRepositoryUpdateMethod()
        {
            // Arrange
            var user = new Fixture().Create<UserDto>();
            var command = new UpdateCommand(user);

            var domainUser = _mapper.Map<User>(user);
            _repositoryMock.Setup(r => r.Get(It.IsAny<long>())).ReturnsAsync(domainUser);

            // Act 
            await _handler.Handle(command);

            // Assert
            _repositoryMock.Verify(r => r.Update(It.IsAny<User>()), Times.Once);
        }

        [Fact]
        public async Task Given_NonExistingUser_When_UpdateCalled_ThrowsNotFoundException()
        {
            // Arrange
            var user = new UserDto();
            var command = new UpdateCommand(user);

            // Act & Assert
            await Assert.ThrowsAsync<NotFoundException>(async () => await _handler.Handle(command));
        }

        class TestHandler : UpdateCommandHandler
        {
            public TestHandler(IUserRepository userRepository, IMapper mapper) : base(userRepository, mapper)
            {
            }

            public Task Handle(UpdateCommand command)
            {
                return base.Handle(command, default);
            }
        }
    }
}
