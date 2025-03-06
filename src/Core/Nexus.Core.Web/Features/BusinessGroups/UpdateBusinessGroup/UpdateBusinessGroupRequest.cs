using Nexus.Core.Application.BusinessGroups.UpdateBusinessGroup;

namespace Nexus.Core.Web.Features.BusinessGroups.UpdateBusinessGroup;

public class UpdateBusinessGroupRequest
{
    public string Name { get; set; } = default!;
    public string? Description { get; set; }
    public string? TaxId { get; set; }

    public UpdateBusinessGroupCommand ToCommand(string id)
    {
        return new UpdateBusinessGroupCommand
        {
            Id = id,
            Name = Name,
            Description = Description,
            TaxId = TaxId
        };
    }
}
