using AutoMapper;
using UserEntity = Nexus.Core.Domain.Users.Aggregates.User;
using UserModel = Nexus.Core.Model.Users.User;

namespace Nexus.Core.Application.TypeAdapters.Profiles;

public class UserProfile : Profile
{
    public UserProfile()
    {
        CreateMap<UserEntity, UserModel>()
            .AfterMap((entity, model) =>
            {
                model.Name = entity?.Person?.Name.GetFullName();
            });
    }
}
