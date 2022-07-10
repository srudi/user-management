using AutoMapper;
using UserManagement.Application.Common;
using Xunit;

namespace UserManagement.Infrastructure.UnitTests
{
    public class AutoMapperTests
    {
        private readonly MapperConfiguration _mapperConfiguration;

        public AutoMapperTests()
        {
            _mapperConfiguration = new MapperConfiguration(cfg =>
            {
                cfg.AddProfile<AutoMapperProfile>();
            });
        }

        [Fact]
        public void ValidateAutoMapperConfiguration()
        {
            _mapperConfiguration.AssertConfigurationIsValid();
        }
    }
}
