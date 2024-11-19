using AutoMapper;

namespace Nexus.Finance.Application.TypeAdapters.Profiles;

internal class AccountsProfile : Profile
{
    public AccountsProfile()
    {
        CreateMap<Domain.Accounts.Aggregates.Account, Model.Accounts.Account>()
            .ForMember(dest => dest.AccountId, opt => opt.MapFrom(src => src.Id))
            .ForMember(dest => dest.Type, opt => opt.MapFrom(src => src.Type.ToString()));

        CreateMap<Domain.Accounts.Aggregates.FinancialInstitution, Model.Accounts.FinancialInstitution>()
            .ForMember(dest => dest.FinancialInstitutionId, opt => opt.MapFrom(src => src.Id));
    }
}
