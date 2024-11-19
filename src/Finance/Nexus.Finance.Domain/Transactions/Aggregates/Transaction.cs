using Goal.Domain.Aggregates;

namespace Nexus.Finance.Domain.Transactions.Aggregates;

public class Transaction : Entity
{
    public Transaction(string name, string email, DateOnly birthdate)
        : this()
    {
        Name = name;
        Email = email;
        Birthdate = birthdate;
    }

    // Empty constructor for EF
    protected Transaction()
        : base() { }

    public string Name { get; private set; } = default!;

    public string Email { get; private set; } = default!;

    public DateOnly Birthdate { get; private set; }

    public void UpdateName(string name)
        => Name = name;

    public void UpdateBirthdate(DateOnly birthdate)
        => Birthdate = birthdate;

    public void UpdateEmail(string email)
        => Email = email;
}
