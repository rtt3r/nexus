using AutoMapper;

namespace Nexus.Core.Application.TypeAdapters.Profiles;

internal class PersonsProfile : Profile
{
    public PersonsProfile()
    {
        CreateMap<Domain.Persons.Aggregates.Person, Model.Persons.NaturalPerson>();

        CreateMap<Domain.Persons.Aggregates.Email, Model.Persons.Email>();

        CreateMap<Domain.Persons.Aggregates.Phone, Model.Persons.PhoneNumber>();

        CreateMap<Domain.Persons.Aggregates.Address, Model.Persons.Address>()
            .ForMember(dest => dest.Type, opt => opt.MapFrom(src => src.Type.ToString()));
    }
}
