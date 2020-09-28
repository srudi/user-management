using AutoMapper;
using Xunit;

namespace UserManagement.Infrastructure.UnitTests.Persistence
{
    public class RepositoryTestFixture
    {
        public IMapper Mapper { get; }

        public RepositoryTestFixture()
        {
            var configurationProvider = new MapperConfiguration(cfg =>
            {
                cfg.AddProfile<AutoMapperProfile>();
            });

            Mapper = configurationProvider.CreateMapper();
        }
    }

    [CollectionDefinition("RepositoryTests")]
    public class QueryCollection : ICollectionFixture<RepositoryTestFixture> { }
}
