using Goal.Application.Commands;
using Nexus.Core.Model.Users;

namespace Nexus.Core.Application.Commands.Users;

public record CreateUserAccountCommand(string? Id, string? Name, string? Email, string? Username) : ICommand<User>
{
}