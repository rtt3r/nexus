using Goal.Domain.Aggregates;

namespace Nexus.Core.Domain.Persons.Aggregates;

public class DocumentType : Entity
{
    protected DocumentType()
        : base()
    {
    }

    public DocumentType(string name)
        : this()
    {
        Name = name;
    }

    public string Name { get; private set; } = default!;
    public string? Mask { get; private set; }
    public string? Description { get; private set; }
    public IEnumerable<Document> Documents { get; private set; } = [];

    public void SetDescription(string? description)
        => Description = description?.Trim();
}
