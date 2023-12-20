namespace Nexus.Core.Api.Controllers.Customers;

public abstract class CustomerRequest
{
    public string Name { get; set; } = null!;
    public DateTime Birthdate { get; set; }
}
