using Goal.Domain.Aggregates;

namespace Nexus.Core.Domain.People.Aggregates;

public class PersonDocumentType : Entity
{
    protected PersonDocumentType()
        : base()
    {
    }

    public PersonDocumentType(string name)
        : this()
    {
        Name = name;
    }

    public string Name { get; private set; } = null!;
    public string? Description { get; private set; }
    public IEnumerable<PersonDocument> Documents { get; private set; } = [];

    public void SetDescription(string? description)
        => Description = description?.Trim();
}
