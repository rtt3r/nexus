using AutoMapper;
using UserAccountModel = Nexus.Core.Model.Users.UserAccount;
using UserAccountEntity = Nexus.Core.Domain.Users.Aggregates.UserAccount;

namespace Nexus.Core.Application.TypeAdapters.Profiles;

public class UserAccount : Profile
{
    public UserAccount()
    {
        CreateMap<UserAccountEntity, UserAccountModel>();
    }
}
