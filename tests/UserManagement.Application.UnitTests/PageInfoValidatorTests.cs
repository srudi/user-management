using FluentAssertions;
using FluentValidation;
using UserManagement.Application.Common;
using Xunit;

namespace UserManagement.Application.UnitTests
{
    public class PageInfoValidatorTests
    {
        private readonly IValidator<PageInfo> _validator = new PageInfoValidator();

        [Theory]
        [InlineData(0, 0, false)]
        [InlineData(1, 0, true)]
        [InlineData(0, 1, false)]
        [InlineData(1, 1, true)]
        public async Task Given_PageInfo_When_ValidateCalled_Then_ReturnsProperIsValid(int pageSize, int pageIndex, bool expectedIsValid)
        {
            // Arrange
            var pageInfo = new PageInfo(pageSize, pageIndex);

            // Act 
            var validationResult = await _validator.ValidateAsync(pageInfo);

            // Assert
            validationResult.IsValid.Should().Be(expectedIsValid);
        }
    }
}
