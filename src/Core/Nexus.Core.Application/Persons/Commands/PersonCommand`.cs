using Goal.Application.Commands;

namespace Nexus.Core.Application.Persons.Commands;

public record PersonCommand<T> : PersonCommand, ICommand<T>
{
}
