using AutoMapper;
using UserProfileEntity = Nexus.Core.Domain.Users.Aggregates.UserProfile;
using UserProfileModel = Nexus.Core.Model.Users.UserProfile;

namespace Nexus.Core.Application.TypeAdapters.Profiles;

public class UserProfile : Profile
{
    public UserProfile()
    {
        CreateMap<UserProfileEntity, UserProfileModel>();
    }
}
