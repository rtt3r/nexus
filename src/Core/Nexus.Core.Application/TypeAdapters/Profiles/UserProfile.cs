using AutoMapper;
using UserProfileModel = Nexus.Core.Model.Users.UserProfile;
using UserProfileEntity = Nexus.Core.Domain.Users.Aggregates.UserProfile;

namespace Nexus.Core.Application.TypeAdapters.Profiles;

public class UserProfile : Profile
{
    public UserProfile()
    {
        CreateMap<UserProfileEntity, UserProfileModel>()
            .ForMember(dest => dest.Status, opt => opt.MapFrom(src => src.Status.ToString()));
    }
}
