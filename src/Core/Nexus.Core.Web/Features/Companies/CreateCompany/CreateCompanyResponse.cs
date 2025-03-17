namespace Nexus.Core.Web.Features.Companies.CreateCompany;

public class CreateCompanyResponse
{
    public string Id { get; set; } = default!;
    public string Name { get; set; } = default!;
    public string? Description { get; set; }
    public string? TaxId { get; set; }
}
