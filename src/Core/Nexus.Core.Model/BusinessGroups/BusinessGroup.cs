namespace Nexus.Core.Model.BusinessGroups;

public class BusinessGroup
{
    public string Id { get; set; } = default!;
    public string Name { get; set; } = default!;
    public string? Description { get; set; }
    public string? TaxId { get; set; }
}
