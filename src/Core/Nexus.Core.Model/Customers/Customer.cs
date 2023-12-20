namespace Nexus.Core.Model.Customers;

public class Customer
{
    public string CustomerId { get; set; } = null!;
    public string Name { get; set; } = null!;
    public string Email { get; set; } = null!;
    public DateTime Birthdate { get; set; }
}
