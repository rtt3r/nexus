using System.Text.RegularExpressions;
using Microsoft.AspNetCore.Routing;

namespace Nexus.Infra.Http.ParameterTransformers;

public partial class ToKebabParameterTransformer : IOutboundParameterTransformer
{
    [GeneratedRegex("([a-z])([A-Z])")]
    private static partial Regex Pattern();

    public string? TransformOutbound(object? value) =>
        value is not null
            ? Pattern().Replace(value.ToString()!, "$1-$2").ToLower() // to kebab 
            : null;

}
