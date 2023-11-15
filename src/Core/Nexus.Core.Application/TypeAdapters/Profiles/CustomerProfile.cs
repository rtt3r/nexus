using AutoMapper;
using Nexus.Core.Domain.Customers.Aggregates;
using CustomerModel = Nexus.Core.Model.Customers.Customer;

namespace Nexus.Core.Application.TypeAdapters.Profiles;

public class CustomerProfile : Profile
{
    public CustomerProfile()
    {
        CreateMap<Customer, CustomerModel>()
            .ForMember(dest => dest.CustomerId, opt => opt.MapFrom(src => src.Id));
    }
}
