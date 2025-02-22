namespace Nexus.Core.Domain.Persons.Aggregates;

public record LegalEntityName(string BrandName, string TradeName)
{
    public string BrandName { get; init; } = BrandName.Trim();
    public string CompanyName { get; init; } = TradeName.Trim();
}
