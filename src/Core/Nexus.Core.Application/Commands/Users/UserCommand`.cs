using Goal.Seedwork.Application.Commands;

namespace Nexus.Core.Application.Commands.Users;

public abstract class UserCommand<T> : UserCommand, ICommand<T>
    where T : ICommandResult
{
}
