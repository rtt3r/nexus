using Nexus.Core.Model.Customers;

namespace Nexus.Core.Application.Customers.Commands;

public record RegisterCustomerCommand(string? Name, string? Email, DateOnly? Birthdate)
    : CustomerCommand<Customer>
{
}
