namespace Nexus.Hcm.Model.People;

public class Employee
{
    public string Name { get; set; } = default!;
    public string Cpf { get; set; } = default!;
    public IList<Contact> Contacts { get; set; } = [];
    public IList<Address> Addresses { get; set; } = [];
}
