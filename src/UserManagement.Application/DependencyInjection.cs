using FluentValidation;
using Microsoft.Extensions.DependencyInjection;
using System.Reflection;
using MediatR;
using UserManagement.Application.Common;
using UserManagement.Application.Services;
using UserManagement.Application.Validators;
using UserManagement.Domain.Entities;
using UserManagement.Application.Users.Queries.GetAll;

namespace UserManagement.Application
{
    public static class DependencyInjection
    {
        public static IServiceCollection AddApplication(this IServiceCollection services)
        {
            services.AddAutoMapper(Assembly.GetExecutingAssembly());
            services.AddMediatR(Assembly.GetExecutingAssembly());
            services.AddScoped<IUserService, UserService>();
            services.AddScoped<IValidator<User>, UserValidator>();
            services.AddScoped<IValidator<PageInfo>, PageInfoValidator>();
            services.AddScoped<IValidator<GetAllPagedQuery>, GetAllPagedQueryValidator>();
            services.AddTransient(typeof(IPipelineBehavior<,>), typeof(ValidationBehaviour<,>));
            return services;
        }
    }
}
