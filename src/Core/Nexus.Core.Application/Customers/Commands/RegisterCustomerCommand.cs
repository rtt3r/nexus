using Nexus.Core.Model.Customers;
using Nexus.Infra.Crosscutting.Errors;
using OneOf;

namespace Nexus.Core.Application.Customers.Commands;

public record RegisterCustomerCommand(string? Name, string? Email, DateOnly? Birthdate)
    : CustomerCommand<OneOf<Customer, AppError>>
{
}
