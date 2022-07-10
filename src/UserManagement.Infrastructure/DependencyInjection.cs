using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using System.Reflection;
using UserManagement.Application.Interfaces;
using UserManagement.Infrastructure.Configuration;
using UserManagement.Infrastructure.Persistence.Contexts;
using UserManagement.Infrastructure.Persistence.Repositories;

namespace UserManagement.Infrastructure
{
    public static class DependencyInjection
    {
        public static IServiceCollection AddInfrastructure(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddAutoMapper(Assembly.GetExecutingAssembly());
            services.Configure<MongoDBConfig>(options => configuration.GetSection("MongoDB").Bind(options));
            services.AddSingleton<IMongoDbContext, MongoDbContext>();
            services.AddSingleton<IUserRepository, UserRepository>();
            return services;
        }
    }
}
