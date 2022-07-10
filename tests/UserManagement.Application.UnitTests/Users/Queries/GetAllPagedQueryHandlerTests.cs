using AutoFixture;
using AutoMapper;
using FluentAssertions;
using Moq;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Threading.Tasks;
using UserManagement.Application.Common;
using UserManagement.Application.Interfaces;
using UserManagement.Application.Users.Dtos;
using UserManagement.Application.Users.Queries.GetAll;
using UserManagement.Domain.Entities;
using Xunit;

namespace UserManagement.Application.UnitTests.Users.Queries
{
    public class GetAllPagedQueryHandlerTests
    {
        private readonly Mock<IUserRepository> _repositoryMock = new Mock<IUserRepository>();
        private readonly IMapper _mapper;
        private readonly GetAllPagedQueryHandler _handler;

        public GetAllPagedQueryHandlerTests()
        {
            var mappingConfig = new MapperConfiguration(c => c.AddMaps(Assembly.GetAssembly(typeof(GetAllPagedQueryHandler))));
            _mapper = mappingConfig.CreateMapper();
            _handler = new GetAllPagedQueryHandler(_repositoryMock.Object, _mapper);
        }

        [Fact]
        public async Task Given_ValidPageInfo_When_GetAllCalled_Then_CallsTheRepositoryGetAllMethod()
        {
            // Arrange
            var users = new Fixture().CreateMany<User>();
            var pageInfo = new PageInfo(10, 0) {TotalCount = users.Count() };
            var expectedUsers = _mapper.Map<IEnumerable<UserDto>>(users);

            var query = new GetAllPagedQuery(pageInfo);
            _repositoryMock.Setup(r => r.GetAll(It.IsAny<PageInfo>(), default)).ReturnsAsync(users);
            _repositoryMock.Setup(r => r.GetTotalCount(default)).ReturnsAsync(1);

            // Act 
            var pagedUsers = await _handler.Handle(query, default);

            // Assert
            _repositoryMock.Verify(r => r.GetAll(pageInfo, default), Times.Once);
            _repositoryMock.Verify(r => r.GetTotalCount(default), Times.Once);
            pagedUsers
                .Should()
                .BeEquivalentTo(new PagedResult<UserDto>(expectedUsers, pageInfo));
        }
    }
}
