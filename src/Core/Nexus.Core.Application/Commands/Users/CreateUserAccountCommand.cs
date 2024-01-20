using Goal.Application.Commands;
using Nexus.Core.Model.Users;

namespace Nexus.Core.Application.Commands.Users;

public class CreateUserAccountCommand
    : ICommand<ICommandResult<UserAccount>>
{
    public CreateUserAccountCommand() { }

    public CreateUserAccountCommand(string id, string name, string email, string username)
        : this()
    {
        Id = id;
        Name = name;
        Email = email;
        Username = username;
    }

    public string Id { get; } = null!;
    public string Name { get; } = null!;
    public string Email { get; } = null!;
    public string Username { get; } = null!;
}