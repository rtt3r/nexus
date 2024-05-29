using Goal.Application.Commands;

namespace Nexus.Core.Application.Commands.Users;

public class UpdateUserProfileCommand
    : ICommand<ICommandResult>
{
    public UpdateUserProfileCommand()
    {
    }

    public UpdateUserProfileCommand(string id, string biography, DateTime? birthdate, string headline)
    {
        Id = id;
        Biography = biography;
        Birthdate = birthdate;
        Headline = headline;
    }

    public string Id { get; } = null!;
    public string Biography { get; } = null!;
    public DateTime? Birthdate { get; } = null!;
    public string Headline { get; } = null!;
}