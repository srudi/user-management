using AutoMapper;
using UserManagement.Application.Users.Queries;
using UserManagement.Domain.Entities;

namespace UserManagement.Application.Common
{
    internal class AutoMapperProfile : Profile
    {
        public AutoMapperProfile()
        {
            CreateMap<UserDto, User>().ReverseMap();
        }
    }
}
