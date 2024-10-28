using Goal.Application.Commands;
using Nexus.Core.Model.Users;

namespace Nexus.Core.Application.Users.Commands;

public record CreateUserCommand(string Id, string Name, string Email, string Username) : ICommand<User>
{
}