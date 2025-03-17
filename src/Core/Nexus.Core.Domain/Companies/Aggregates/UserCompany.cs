using Goal.Domain.Aggregates;

namespace Nexus.Core.Domain.Companies.Aggregates;

public sealed class UserCompany : Entity
{
    public string UserId { get; private set; } = default!;
    public string CompanyId { get; private set; } = default!;
    public CompanyRole RoleInCompany { get; private set; } = default!;
    public DateTime AssignedAt { get; private set; } = DateTime.UtcNow;
    public Company Company { get; private set; } = default!;

    private UserCompany()
        : base()
    {
    }

    public UserCompany(Company company, string userId, CompanyRole companyRole)
        : this()
    {
        UserId = userId;
        SetCompany(company);
        SetRoleInCompany(companyRole);
    }

    private void SetCompany(Company company)
    {
        ArgumentNullException.ThrowIfNull(company, nameof(company));

        Company = company;
        CompanyId = company.Id;
    }

    public void SetRoleInCompany(CompanyRole companyRole)
    {
        RoleInCompany = companyRole;
        AssignedAt = DateTime.UtcNow;
    }
}
