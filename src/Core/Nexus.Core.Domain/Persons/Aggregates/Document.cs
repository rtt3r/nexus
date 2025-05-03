using Goal.Domain.Aggregates;

namespace Nexus.Core.Domain.Persons.Aggregates;

public sealed class Document : Entity
{
    private Document()
        : base()
    {
    }

    public Document(DocumentType type, string number)
    {
        ArgumentException.ThrowIfNullOrWhiteSpace(number, nameof(number));

        Type = type;
        Number = number;
    }

    public string PersonId { get; private set; } = default!;
    public DocumentType Type { get; private set; } = default!;
    public string Number { get; private set; } = default!;
    public bool Active { get; private set; } = true;
    public Person Person { get; private set; } = default!;

    public void Activate()
        => Active = true;

    public void Inactivate()
        => Active = false;
}
