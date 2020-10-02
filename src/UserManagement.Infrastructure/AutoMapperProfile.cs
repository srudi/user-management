using AutoMapper;
using DomainUser = UserManagement.Domain.Entities.User;
using DomainCompany = UserManagement.Domain.ValueObjects.Company;
using DomainGeo = UserManagement.Domain.Entities.Geo;
using DomainAddress = UserManagement.Domain.ValueObjects.Address;
using UserManagement.Infrastructure.Persistence.Contexts.Models;

namespace UserManagement.Infrastructure
{
    class AutoMapperProfile : Profile
    {
        public AutoMapperProfile()
        {
            CreateMap<DomainUser, User>().ForMember(x => x.InternalId, opt=>opt.Ignore()).ReverseMap();
            CreateMap<DomainCompany, Company>().ReverseMap();
            CreateMap<DomainGeo, Geo>().ReverseMap();
            CreateMap<DomainAddress, Address>().ReverseMap();
        }
    }
}
