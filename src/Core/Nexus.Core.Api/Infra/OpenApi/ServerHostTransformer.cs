using Microsoft.AspNetCore.OpenApi;
using Microsoft.OpenApi.Models;

namespace Nexus.Core.Api.Infra.OpenApi;

internal sealed class ServerHostTransformer : IOpenApiDocumentTransformer
{
    private readonly IHttpContextAccessor _httpContextAccessor;

    public ServerHostTransformer(IHttpContextAccessor httpContextAccessor)
    {
        _httpContextAccessor = httpContextAccessor;
    }

    public Task TransformAsync(OpenApiDocument document, OpenApiDocumentTransformerContext context, CancellationToken cancellationToken)
    {
        HttpContext? httpContext = _httpContextAccessor.HttpContext;

        if (httpContext == null)
        {
            return Task.CompletedTask;
        }

        string scheme = httpContext.Request.Scheme ?? "http";
        string host = httpContext.Request.Host.Value ?? "localhost";

        document.Servers =
        [
            new OpenApiServer { Url = $"{scheme}://{host}" }
        ];

        return Task.CompletedTask;
    }
}
