using Goal.Application.Commands;

namespace Nexus.Core.Application.Commands.Users;

public record UpdateUserProfileCommand(string? Id, string? Biography, DateTime? Birthdate, string? Headline)
    : ICommand<ICommandResult>
{
}