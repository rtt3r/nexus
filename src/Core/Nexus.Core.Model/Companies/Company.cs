namespace Nexus.Core.Model.Companies;

public class Company
{
    public string CompanyId { get; set; } = default!;
    public string CompanyName { get; set; } = default!;
    public string BrandingName { get; set; } = default!;
    public string Cnpj { get; set; } = default!;
    public string CompanyType { get; set; } = default!;
    public IList<Company> Branches { get; set; } = [];
}
