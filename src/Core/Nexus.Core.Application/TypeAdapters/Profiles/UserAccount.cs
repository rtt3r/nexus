using AutoMapper;
using UserAccountEntity = Nexus.Core.Domain.Users.Aggregates.UserAccount;
using UserAccountModel = Nexus.Core.Model.Users.UserAccount;

namespace Nexus.Core.Application.TypeAdapters.Profiles;

public class UserAccount : Profile
{
    public UserAccount()
    {
        CreateMap<UserAccountEntity, UserAccountModel>();
    }
}
