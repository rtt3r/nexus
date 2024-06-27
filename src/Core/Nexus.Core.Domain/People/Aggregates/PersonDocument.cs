using Goal.Domain.Aggregates;

namespace Nexus.Core.Domain.People.Aggregates;

public class PersonDocument : Entity
{
    private PersonDocument()
        : base()
    {
    }

    private PersonDocument(PersonDocumentType type, string number)
    {
        Type = type;
        TypeId = type.Id;
        Number = number;
    }

    public string PersonId { get; private set; } = null!;
    public string TypeId { get; private set; } = null!;
    public string Number { get; private set; } = null!;
    public string Issuer { get; private set; } = null!;
    public DateTime? IssuedAt { get; private set; } = null!;
    public DateTime? ValidUntil { get; private set; } = null!;
    public Person Person { get; private set; } = null!;
    public PersonDocumentType Type { get; private set; } = null!;

    public void UpdateIssuer(string issuer)
        => Issuer = issuer;

    public void UpdateIssuer(string issuer, DateTime issuedAt)
    {
        UpdateIssuer(issuer);
        IssuedAt = issuedAt;
    }

    public void UpdateValidity(DateTime validUntil)
        => ValidUntil = validUntil;

    public static PersonDocument CreateDocument(PersonDocumentType type, string number)
        => new(type, number);
}
