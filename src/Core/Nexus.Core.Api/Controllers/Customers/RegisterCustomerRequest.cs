namespace Nexus.Core.Api.Controllers.Customers;

public class RegisterCustomerRequest : CustomerRequest
{
    public string? Email { get; set; } = null;
}
