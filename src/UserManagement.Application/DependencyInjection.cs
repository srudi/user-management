using FluentValidation;
using Microsoft.Extensions.DependencyInjection;
using UserManagement.Application.Services;
using UserManagement.Application.Validators;
using UserManagement.Domain.Entities;

namespace UserManagement.Application
{
    public static class DependencyInjection
    {
        public static IServiceCollection AddApplication(this IServiceCollection services)
        {
            services.AddScoped<IUserService, UserService>();
            services.AddScoped<IValidator<User>, UserValidator>();
            return services;
        }
    }
}
