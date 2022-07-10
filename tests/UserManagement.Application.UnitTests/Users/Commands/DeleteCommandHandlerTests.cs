using AutoMapper;
using Moq;
using System.Reflection;
using System.Threading.Tasks;
using UserManagement.Application.Exceptions;
using UserManagement.Application.Interfaces;
using UserManagement.Application.Users.Commands.Delete;
using UserManagement.Domain.Entities;
using Xunit;
namespace UserManagement.Application.UnitTests.Users.Commands
{
    public class DeleteCommandHandlerTests
    {
        private readonly Mock<IUserRepository> _repositoryMock = new Mock<IUserRepository>();
        private readonly IMapper _mapper;
        private readonly TestHandler _handler;

        public DeleteCommandHandlerTests()
        {
            var mappingConfig = new MapperConfiguration(c => c.AddMaps(Assembly.GetAssembly(typeof(DeleteCommand))));
            _mapper = mappingConfig.CreateMapper();
            _handler = new TestHandler(_repositoryMock.Object, _mapper);
        }

        [Fact]
        public async Task Given_UserId_Delete_CallsTheRepositoryDeleteMethod()
        {
            // Arrange
            long userId = 1;
            var command = new DeleteCommand(userId);
            _repositoryMock.Setup(r => r.Get(userId)).ReturnsAsync(new User());

            // Act 
            await _handler.Handle(command);

            // Assert
            _repositoryMock.Verify(r => r.Delete(userId), Times.Once);
        }

        [Fact]
        public async Task Given_UserIdButDoesNotExist_Delete_ThrowsNotFoundException()
        {
            // Arrange
            long userId = 1;
            var command = new DeleteCommand(userId);

            // Act & Assert
            await Assert.ThrowsAsync<NotFoundException>(async () => await _handler.Handle(command));
        }


        class TestHandler : DeleteCommandHandler
        {
            public TestHandler(IUserRepository userRepository, IMapper mapper) : base(userRepository, mapper)
            {
            }

            public Task Handle(DeleteCommand command)
            {
                return base.Handle(command, default);
            }
        }
    }
}
