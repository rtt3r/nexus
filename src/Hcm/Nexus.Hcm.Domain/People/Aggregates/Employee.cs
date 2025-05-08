namespace Nexus.Hcm.Domain.People.Aggregates;

public class Employee : NaturalPerson
{
    protected Employee()
        : base()
    {
    }

    public Employee(string name)
        : base(name)
    {
    }
}