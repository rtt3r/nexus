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
            .ForMember(dest => dest.CompanyType, opt => opt.MapFrom(src => src.CompanyType.ToString()))
            .ForMember(dest => dest.MunicipalRegistration, opt => opt.ConvertUsing(new CreateCompanyDocumentConverter(Domain.Persons.Aggregates.DocumentType.MunicipalRegistration)))
            .ForMember(dest => dest.StateRegistration, opt => opt.ConvertUsing(new CreateCompanyDocumentConverter(Domain.Persons.Aggregates.DocumentType.StateRegistration)));

        CreateMap<Domain.Persons.Aggregates.Address, Address>()
            .ForMember(dest => dest.Type, opt => opt.MapFrom(src => src.Type.ToString()));

        CreateMap<Domain.Persons.Aggregates.Contact, Contact>()
            .ForMember(dest => dest.Type, opt => opt.MapFrom(src => src.Type.ToString()));
    }
}

internal class CreateCompanyDocumentConverter(Domain.Persons.Aggregates.DocumentType documentType)
    : IValueConverter<Domain.Companies.Aggregates.Company, string?>
{
    public string? Convert(Domain.Companies.Aggregates.Company sourceMember, ResolutionContext context)
        => sourceMember.Documents.FirstOrDefault(d => d.Type == documentType)?.Number;
}
