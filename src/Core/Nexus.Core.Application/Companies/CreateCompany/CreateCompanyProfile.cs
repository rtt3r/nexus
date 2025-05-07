using AutoMapper;
using Nexus.Core.Model.Companies;
using Nexus.Core.Model.People;

namespace Nexus.Core.Application.Companies.CreateCompany;

internal class CreateCompanyProfile : Profile
{
    public CreateCompanyProfile()
    {
        CreateMap<Domain.Companies.Aggregates.Company, Company>()
            .ForMember(dest => dest.CompanyId, opt => opt.MapFrom(src => src.Id))
            .ForMember(dest => dest.CompanyName, opt => opt.MapFrom(src => src.Name))
            .ForMember(dest => dest.BrandingName, opt => opt.MapFrom(src => src.BrandName))
            .ForMember(dest => dest.CompanyId, opt => opt.MapFrom(src => src.Id))
            .ForMember(dest => dest.CompanyType, opt => opt.MapFrom(src => src.CompanyType.ToString()))
            .ForMember(dest => dest.Cnpj, opt => opt.MapFrom<CnpjResolver>())
            .ForMember(dest => dest.MunicipalRegistration, opt => opt.MapFrom<MunicipalRegistrationResolver>())
            .ForMember(dest => dest.StateRegistration, opt => opt.MapFrom<StateRegistrationResolver>());

        CreateMap<Domain.Persons.Aggregates.PersonAddress, Address>()
            .ForMember(dest => dest.Type, opt => opt.MapFrom(src => src.Type.ToString()));

        CreateMap<Domain.Persons.Aggregates.PersonContact, Contact>()
            .ForMember(dest => dest.Type, opt => opt.MapFrom(src => src.Type.ToString()));
    }
}

internal class CnpjResolver : IValueResolver<Domain.Companies.Aggregates.Company, Company, string>
{
    public string Resolve(Domain.Companies.Aggregates.Company source, Company destination, string destMember, ResolutionContext context)
        => source.Documents.First(d => d.Document.Name == "Cnpj").Value;
}

internal class MunicipalRegistrationResolver : IValueResolver<Domain.Companies.Aggregates.Company, Company, string?>
{
    public string? Resolve(Domain.Companies.Aggregates.Company source, Company destination, string? destMember, ResolutionContext context)
        => source.Documents.FirstOrDefault(d => d.Document.Name == "MunicipalRegistration")?.Value;
}

internal class StateRegistrationResolver : IValueResolver<Domain.Companies.Aggregates.Company, Company, string?>
{
    public string? Resolve(Domain.Companies.Aggregates.Company source, Company destination, string? destMember, ResolutionContext context)
        => source.Documents.FirstOrDefault(d => d.Document.Name == "StateRegistration")?.Value;
}

