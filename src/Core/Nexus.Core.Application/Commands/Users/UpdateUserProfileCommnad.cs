using Goal.Seedwork.Application.Commands;

namespace Nexus.Core.Application.Commands.Users;

public class UpdateUserProfileCommand : Command<ICommandResult>
{
    public required string Id { get; set; }
    public string Biography { get; set; }
    public DateTime? Birthdate { get; set; }
    public string Headline { get; set; }
}