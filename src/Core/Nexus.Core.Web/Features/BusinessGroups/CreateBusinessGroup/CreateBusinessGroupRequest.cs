using Nexus.Core.Application.BusinessGroups.CreateBusinessGroup;

namespace Nexus.Core.Web.Features.BusinessGroups.CreateBusinessGroup;

public class CreateBusinessGroupRequest
{
    public string Name { get; set; } = default!;
    public string? Description { get; set; }
    public string? TaxId { get; set; }

    public CreateBusinessGroupCommand ToCommand()
    {
        return new CreateBusinessGroupCommand
        {
            Name = Name,
            Description = Description,
            TaxId = TaxId
        };
    }
}
