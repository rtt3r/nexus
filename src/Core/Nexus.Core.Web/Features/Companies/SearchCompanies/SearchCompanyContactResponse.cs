namespace Nexus.Core.Web.Features.Companies.SearchCompanies;

public sealed class SearchCompanyContactResponse
{
    public string Type { get; set; } = default!;
    public string Name { get; set; } = default!;
    public string Email { get; set; } = default!;
    public string LandlinePhone { get; set; } = default!;
    public string MobilePhone { get; set; } = default!;
    public string Whatsapp { get; set; } = default!;
}
