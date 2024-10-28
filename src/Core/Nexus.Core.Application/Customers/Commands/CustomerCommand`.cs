using Goal.Application.Commands;

namespace Nexus.Core.Application.Customers.Commands;

public record CustomerCommand<T> : CustomerCommand, ICommand<T>
{
}
