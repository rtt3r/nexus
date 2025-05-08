namespace Nexus.Hcm.Api.Features.Employees.SearchEmployees;

public class SearchEmployeeResponse
{
    public string Name { get; set; } = default!;
    public string Cpf { get; set; } = default!;
    public IList<SearchEmployeeContactResponse> Contacts { get; set; } = [];
    public IList<SearchEmployeeAddressResponse> Addresses { get; set; } = [];
}
