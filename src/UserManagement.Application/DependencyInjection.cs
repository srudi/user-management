using FluentValidation;
using Microsoft.Extensions.DependencyInjection;
using System.Reflection;
using MediatR;
using UserManagement.Application.Common;
using UserManagement.Application.Validators;
using UserManagement.Application.Users.Queries.GetAll;
using UserManagement.Application.Users.Dtos;
using UserManagement.Application.Users.Commands.Update;

namespace UserManagement.Application
{
    public static class DependencyInjection
    {
        public static IServiceCollection AddApplication(this IServiceCollection services)
        {
            services.AddAutoMapper(Assembly.GetExecutingAssembly());
            services.AddMediatR(Assembly.GetExecutingAssembly());
            services.AddScoped<IValidator<UserDto>, UserValidator>();
            services.AddScoped<IValidator<PageInfo>, PageInfoValidator>();
            services.AddScoped<IValidator<GetAllPagedQuery>, GetAllPagedQueryValidator>();
            services.AddScoped<IValidator<UpdateCommand>, UpdateCommandValidator>();
            services.AddTransient(typeof(IPipelineBehavior<,>), typeof(ValidationBehaviour<,>));
            return services;
        }
    }
}
