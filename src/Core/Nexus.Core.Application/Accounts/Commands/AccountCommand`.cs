using Goal.Application.Commands;

namespace Nexus.Core.Application.Accounts.Commands;

public record AccountCommand<T> : AccountCommand, ICommand<T>
{
}
