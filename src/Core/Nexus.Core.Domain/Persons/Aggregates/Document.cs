using Goal.Domain.Aggregates;

namespace Nexus.Core.Domain.Persons.Aggregates;

public class Document : Entity
{
    private Document()
        : base()
    {
    }

    private Document(DocumentType type, string number)
    {
        Type = type;
        TypeId = type.Id;
        Number = number;
    }

    public string PersonId { get; private set; } = default!;
    public string TypeId { get; private set; } = default!;
    public string Number { get; private set; } = default!;
    public Person Person { get; private set; } = default!;
    public DocumentType Type { get; private set; } = default!;

    public static Document CreateDocument(DocumentType type, string number)
        => new(type, number);
}
