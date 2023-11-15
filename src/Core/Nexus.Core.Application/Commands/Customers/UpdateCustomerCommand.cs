using Goal.Seedwork.Application.Commands;

namespace Nexus.Core.Application.Commands.Customers;

public class UpdateCustomerCommand : CustomerCommand<ICommandResult>
{
    public string CustomerId { get; set; }

    public UpdateCustomerCommand(string customerId, string name, DateTime birthDate)
    {
        CustomerId = customerId;
        Name = name;
        Birthdate = birthDate;
    }
}
