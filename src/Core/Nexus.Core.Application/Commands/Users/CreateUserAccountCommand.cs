using Goal.Seedwork.Application.Commands;
using Nexus.Core.Model.Users;

namespace Nexus.Core.Application.Commands.Users;

public class CreateUserAccountCommand : Command<ICommandResult<UserAccount>>
{
    public required string Id { get; set; }
    public required string Name { get; set; }
    public required string Email { get; set; }
    public required string Username { get; set; }
}