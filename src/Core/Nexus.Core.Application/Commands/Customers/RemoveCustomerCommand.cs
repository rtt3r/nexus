using Goal.Seedwork.Application.Commands;

namespace Nexus.Core.Application.Commands.Customers;

public class RemoveCustomerCommand : CustomerCommand<ICommandResult>
{
    public string CustomerId { get; set; }

    public RemoveCustomerCommand(string customerId)
    {
        CustomerId = customerId;
    }
}
