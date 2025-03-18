namespace Nexus.Core.Web.Features.Companies.CreateCompany;

public sealed class CreateCompanyContactResponse
{
    public string Type { get; set; } = default!;
    public string Name { get; set; } = default!;
    public string Email { get; set; } = default!;
    public string LandlinePhone { get; set; } = default!;
    public string MobilePhone { get; set; } = default!;
    public string Whatsapp { get; set; } = default!;
}
