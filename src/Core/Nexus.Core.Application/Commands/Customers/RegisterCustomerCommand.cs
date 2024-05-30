using Goal.Application.Commands;
using Nexus.Core.Model.Customers;

namespace Nexus.Core.Application.Commands.Customers;

public record RegisterCustomerCommand(string? Name, string? Email, DateTime? Birthdate)
    : CustomerCommand<ICommandResult<Customer>>
{
}
