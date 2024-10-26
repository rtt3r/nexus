using Goal.Domain.Aggregates;

namespace Nexus.Core.Domain.People.Aggregates;

public class PersonEmail : Entity
{
    protected PersonEmail()
        : base()
    {
    }

    public PersonEmail(string mailAddress)
        : this()
    {
        MailAddress = mailAddress;
    }

    public string PersonId { get; private set; } = null!;
    public string MailAddress { get; private set; } = null!;
    public bool Principal { get; private set; }
    public Person Person { get; private set; } = null!;

    public void SetPrincipal(bool principal)
        => Principal = principal;
}