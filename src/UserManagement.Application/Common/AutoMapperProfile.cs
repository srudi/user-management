using AutoMapper;
using UserManagement.Application.Users.Dtos;
using UserManagement.Domain.Entities;
using UserManagement.Domain.ValueObjects;

namespace UserManagement.Application.Common
{
    internal class AutoMapperProfile : Profile
    {
        public AutoMapperProfile()
        {
            CreateMap<UserDto, User>().ReverseMap();
            CreateMap<CompanyDto, Company>().ReverseMap();
            CreateMap<GeoDto, Geo>().ReverseMap();
            CreateMap<AddressDto, Address>().ReverseMap();
        }
    }
}
