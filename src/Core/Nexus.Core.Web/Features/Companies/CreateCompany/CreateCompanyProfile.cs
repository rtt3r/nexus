using AutoMapper;
using Nexus.Core.Model.Companies;

namespace Nexus.Core.Web.Features.Companies.CreateCompany;

public class CreateCompanyProfile : Profile
{
    public CreateCompanyProfile()
    {
        CreateMap<Company, CreateCompanyResponse>();
    }
}
