using Microsoft.AspNetCore.OpenApi;
using Microsoft.OpenApi.Models;
using Nexus.Infra.Crosscutting.Extensions;

namespace Nexus.Finance.Api.Infra.OpenApi;

internal class SnakeCaseSchemaTransformer : IOpenApiSchemaTransformer
{
    public async Task TransformAsync(OpenApiSchema schema, OpenApiSchemaTransformerContext context, CancellationToken cancellationToken)
    {
        schema.Properties = TransformProperties(schema.Properties);

        await Task.CompletedTask;
    }

    public static IDictionary<string, OpenApiSchema> TransformProperties(IDictionary<string, OpenApiSchema> properties)
    {
        Dictionary<string, OpenApiSchema> result = [];

        foreach (var item in properties)
        {
            if (item.Value.Properties.Count > 0)
            {
                item.Value.Properties = TransformProperties(item.Value.Properties);
            }

            result.Add(item.Key.ToSnakeCase(), item.Value);
        }

        return result;
    }
}
