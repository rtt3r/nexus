using AutoMapper;
using Nexus.Core.Model.Companies;
using Nexus.Core.Model.People;

namespace Nexus.Core.Web.Features.Companies.CreateCompany;

public class CreateCompanyProfile : Profile
{
    public CreateCompanyProfile()
    {
        CreateMap<Company, CreateCompanyResponse>();
        CreateMap<Address, CreateCompanyAddressResponse>();
        CreateMap<Contact, CreateCompanyContactResponse>();
    }
}