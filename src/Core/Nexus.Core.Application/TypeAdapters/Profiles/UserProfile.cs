using AutoMapper;
using Nexus.Core.Domain.Users.Events;
using UserEntity = Nexus.Core.Domain.Users.Aggregates.User;
using UserModel = Nexus.Core.Model.Users.User;

namespace Nexus.Core.Application.TypeAdapters.Profiles;

public class UserProfile : Profile
{
    public UserProfile()
    {
        CreateMap<UserEntity, UserModel>()
            .ForMember(dest => dest.UserId, opt => opt.MapFrom(src => src.Id));

        CreateMap<UserRegisteredEvent, UserModel>()
            .ForMember(dest => dest.UserId, opt => opt.MapFrom(src => src.AggregateId));
    }
}
