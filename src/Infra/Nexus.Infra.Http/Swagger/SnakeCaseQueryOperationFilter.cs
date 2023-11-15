using Nexus.Infra.Crosscutting.Extensions;
using Microsoft.OpenApi.Models;
using Swashbuckle.AspNetCore.SwaggerGen;

namespace Nexus.Infra.Http.Swagger;

public class SnakeCaseQueryOperationFilter : IOperationFilter
{
    public void Apply(OpenApiOperation operation, OperationFilterContext context)
    {
        foreach (OpenApiParameter item in operation.Parameters ?? new List<OpenApiParameter>())
        {
            item.Name = item.Name.ToSnakeCase();
        }
    }
}
