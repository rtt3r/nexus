using System.Reflection;

namespace Nexus.Core.Application.Extensions.DependencyInjection;

public sealed class CoreApplicationOptions
{
    public Assembly[] MediatRAssemblies { get; private set; } = [];

    public void RegisterMediatRFromAssemblies(params Assembly[] assemblies) => MediatRAssemblies = assemblies ?? [];

}
