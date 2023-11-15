using AutoMapper;
using Nexus.Core.Domain.Users.Aggregates;
using UserModel = Nexus.Core.Model.Users.User;

namespace Nexus.Core.Application.TypeAdapters.Profiles;

public class UserProfile : Profile
{
    public UserProfile()
    {
        CreateMap<User, UserModel>()
            .ForMember(dest => dest.Status, opt => opt.MapFrom(src => src.Status.HasValue ? src.Status.ToString() : null));
    }
}
