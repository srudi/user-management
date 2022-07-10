using FluentAssertions;
using FluentValidation;
using System.Threading.Tasks;
using UserManagement.Application.Users.Dtos;
using UserManagement.Application.Validators;
using Xunit;

namespace UserManagement.Application.UnitTests.Users.Commands
{
    public class CreateCommandValidatorTests
    {
        private readonly IValidator<UserDto> _validator = new UserValidator();

        [Fact]
        public async Task Given_InvalidUser_When_CreateCalled_Then_ShouldRetrunsValidationErrors()
        {
            // Arrange
            var user = new UserDto();

            // Act
            var validationResult = await _validator.ValidateAsync(user);

            // Assert
            validationResult.IsValid.Should().BeFalse();
            validationResult.Errors.Should().NotBeEmpty();
        }
    }
}
