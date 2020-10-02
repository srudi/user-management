using FluentValidation;
using FluentValidation.Results;
using Moq;
using System.Threading;
using System.Threading.Tasks;
using UserManagement.Application.Common;
using UserManagement.Application.Exceptions;
using UserManagement.Application.Interfaces;
using UserManagement.Application.Services;
using UserManagement.Domain.Entities;
using Xunit;

namespace UserManagement.Application.UnitTests
{
    public class UserServiceTests
    {
        private readonly UserService _userService;
        private readonly Mock<IUserRepository> _repositoryMock = new Mock<IUserRepository>();
        private readonly Mock<IValidator<User>> _validatorMock = new Mock<IValidator<User>>();
        private readonly Mock<ValidationResult> _validationResultMock = new Mock<ValidationResult>();

        public UserServiceTests()
        {
            _validatorMock.Setup(v => v.Validate(It.IsAny<User>())).Returns(_validationResultMock.Object);
            _userService = new UserService(_repositoryMock.Object, _validatorMock.Object);
        }

        [Fact]
        public async Task Given_ValidUserId_GetAll_CallsTheRepositoryGetAllMethod()
        {
            // Arrange
            var pageInfo = new PageInfo(0, 0);

            // Act 
            await _userService.GetAll(pageInfo,default);

            // Assert
            _repositoryMock.Verify(r => r.GetAll(pageInfo, default), Times.Once);
        }


        [Fact]
        public async Task Given_ValidUserId_Get_CallsTheRepositoryGetMethod()
        {
            // Arrange
            long userId = 1;
            _repositoryMock.Setup(r => r.Get(userId)).ReturnsAsync(new User());

            // Act 
            await _userService.Get(userId);

            // Assert
            _repositoryMock.Verify(r => r.Get(userId), Times.Once);
        }

        [Fact]
        public async Task Given_InvalidUserId_Get_ThrowsNotFoundException()
        {
            long userId = 0;
            await Assert.ThrowsAsync<NotFoundException>(async () => await _userService.Get(userId));
        }

        [Fact]
        public async Task Given_ValidUser_Create_CallsTheRepositoryCreateMethod()
        {
            // Arrange
            var user = new User();
            _validationResultMock.Setup(r => r.IsValid).Returns(true);

            // Act 
            await _userService.Create(user);

            // Assert
            _repositoryMock.Verify(r => r.Create(user), Times.Once);
        }

        [Fact]
        public async Task Given_InvalidUser_Create_ThrowsException()
        {
            // Arrange
            var user = new User();
            _validationResultMock.Setup(r => r.IsValid).Returns(false);


            // Act & Assert
            await Assert.ThrowsAsync<ValidationException>(async () => await _userService.Create(user));
        }

        [Fact]
        public async Task Given_ValidUser_Update_CallsTheRepositoryUpdateMethod()
        {
            // Arrange
            var user = new User();
            _validationResultMock.Setup(r => r.IsValid).Returns(true);
            _repositoryMock.Setup(r => r.Get(It.IsAny<long>())).ReturnsAsync(user);

            // Act 
            await _userService.Update(user);
            
            // Assert
            _repositoryMock.Verify(r => r.Update(user), Times.Once);
        }


        [Fact]
        public async Task Given_ValidUserButDoesNotExist_Update_ThrowsNotFoundException()
        {
            // Arrange
            var user = new User();
            _validationResultMock.Setup(r => r.IsValid).Returns(true);

            // Act & Assert
            await Assert.ThrowsAsync<NotFoundException>(async () => await _userService.Update(user));
        }

        [Fact]
        public async Task Given_InvalidUser_Update_ThrowValidationException()
        {
            // Arrange
            var user = new User();
            _validationResultMock.Setup(r => r.IsValid).Returns(false);

            // Act & Assert
            await Assert.ThrowsAsync<ValidationException>(async () => await _userService.Update(user));
        }

        [Fact]
        public async Task Given_UserId_Delete_CallsTheRepositoryDeleteMethod()
        {
            // Arrange
            long userId = 1;
            _repositoryMock.Setup(r => r.Get(userId)).ReturnsAsync(new User());

            // Act 
            await _userService.Delete(userId);

            // Assert
            _repositoryMock.Verify(r => r.Delete(userId), Times.Once);
        }

        [Fact]
        public async Task Given_UserIdButDoesNotExist_Delete_ThrowsNotFoundException()
        {
            // Arrange
            long userId = 1;

            // Act & Assert
            await Assert.ThrowsAsync<NotFoundException>(async () => await _userService.Delete(userId));
        }
    }
}
