using Goal.Seedwork.Application.Commands;
using Nexus.Core.Model.Customers;

namespace Nexus.Core.Application.Commands.Customers;

public record RegisterNewCustomerCommand(string Name, string Email, DateTime Birthdate)
    : CustomerCommand<ICommandResult<Customer>>
{
}
