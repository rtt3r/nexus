using AutoMapper;
using Nexus.Core.Model.Companies;
using Nexus.Core.Model.People;

namespace Nexus.Core.Web.Features.Companies.GetCompany;

public class GetCompanyProfile : Profile
{
    public GetCompanyProfile()
    {
        CreateMap<Company, GetCompanyResponse>();
        CreateMap<Address, GetCompanyAddressResponse>();
        CreateMap<Contact, GetCompanyContactResponse>();
    }
}
