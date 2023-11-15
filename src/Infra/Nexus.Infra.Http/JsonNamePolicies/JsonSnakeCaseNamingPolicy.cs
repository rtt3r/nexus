using System.Text.Json;
using Nexus.Infra.Crosscutting.Extensions;

namespace Nexus.Infra.Http.JsonNamePolicies;

public class JsonSnakeCaseNamingPolicy : JsonNamingPolicy
{
    public override string ConvertName(string name)
        => name.ToSnakeCase();
}
