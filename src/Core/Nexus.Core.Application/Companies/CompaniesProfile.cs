using AutoMapper;
using Nexus.Core.Model.Companies;

namespace Nexus.Core.Application.Companies;

internal class CompaniesProfile : Profile
{
    public CompaniesProfile()
    {
        CreateMap<Domain.Companies.Aggregates.Company, Model.Companies.Company>();
        CreateMap<Domain.Companies.Aggregates.Company, Company>()
            .ForMember(dest => dest.CompanyType, opt => opt.MapFrom(src => src.CompanyType.ToString()));
    }
}
