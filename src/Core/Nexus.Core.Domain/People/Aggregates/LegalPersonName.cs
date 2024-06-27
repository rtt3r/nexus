namespace Nexus.Core.Domain.People.Aggregates;

public record LegalPersonName(string BrandName, string CorporateName)
{
    public string BrandName { get; init; } = BrandName.Trim();
    public string CorporateName { get; init; } = CorporateName.Trim();
}
