namespace Nexus.Core.Model.Persons;

public class NaturalPerson
{
    public string Id { get; set; } = default!;
    public string FisrtName { get; set; } = default!;
    public string LastName { get; set; } = default!;
    public string Cpf { get; set; } = default!;
    public string? Gender { get; set; } = default!;
    public DateOnly? Birthdate { get; set; } = default!;
    public IList<Email> Emails { get; set; } = [];
    public IList<PhoneNumber> PhoneNumbers { get; set; } = [];
    public IList<Address> Addresses { get; set; } = [];
}
