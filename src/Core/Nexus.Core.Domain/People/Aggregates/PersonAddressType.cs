using Nexus.Core.Domain.Shared;

namespace Nexus.Core.Domain.People.Aggregates;

public class PersonAddressType : EntityTypeDescriptor
{
    private PersonAddressType()
        : base()
    {
    }

    public PersonAddressType(string name)
        : base(name)
    {
    }

    public IEnumerable<PersonAddress> Addresses { get; private set; } = [];
}
