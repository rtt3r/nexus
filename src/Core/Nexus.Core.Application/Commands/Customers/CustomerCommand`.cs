using Goal.Application.Commands;

namespace Nexus.Core.Application.Commands.Customers;

public record CustomerCommand<T> : CustomerCommand, ICommand<T>
    where T : ICommandResult
{
}
