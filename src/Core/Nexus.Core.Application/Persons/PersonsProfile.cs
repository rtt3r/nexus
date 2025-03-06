using AutoMapper;

namespace Nexus.Core.Application.Persons;

internal class PersonsProfile : Profile
{
    public PersonsProfile()
    {
        CreateMap<Domain.Persons.Aggregates.Person, Model.Persons.NaturalPerson>();
        //CreateMap<Domain.Persons.Aggregates.Document, Model.Persons.>();
        CreateMap<Domain.Persons.Aggregates.Contact, Model.Persons.PhoneNumber>();
        CreateMap<Domain.Persons.Aggregates.Address, Model.Persons.Address>()
            .ForMember(dest => dest.Type, opt => opt.MapFrom(src => src.Type.ToString()));
    }
}
