using Goal.Seedwork.Application.Commands;
using Nexus.Core.Model.Users;

namespace Nexus.Core.Application.Commands.Users;

public class CreateUserProfileCommand : Command<ICommandResult<UserProfile>>
{
    public required string Id { get; set; }
}