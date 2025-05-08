namespace Nexus.Hcm.Api.Features.Employees.GetEmployee;

public class GetEmployeeResponse
{
    public string Name { get; set; } = default!;
    public string Cpf { get; set; } = default!;
    public IList<GetEmployeeContactResponse> Contacts { get; set; } = [];
    public IList<GetEmployeeAddressResponse> Addresses { get; set; } = [];
}
