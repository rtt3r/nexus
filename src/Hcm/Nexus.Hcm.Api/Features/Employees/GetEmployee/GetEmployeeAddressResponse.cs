namespace Nexus.Hcm.Api.Features.Employees.GetEmployee;

public sealed class GetEmployeeAddressResponse
{
    public string Type { get; set; } = default!;
    public string ZipCode { get; set; } = default!;
    public string Street { get; set; } = default!;
    public string Number { get; set; } = default!;
    public string? Complement { get; set; }
    public string Neighborhood { get; set; } = default!;
    public string City { get; set; } = default!;
    public string State { get; set; } = default!;
    public string Country { get; set; } = default!;
}
