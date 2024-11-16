using System.Reflection;

namespace Nexus.Core.Application.DependencyInjection;

public sealed class CoreApplicationOptions
{
    public Assembly[] MediatRAssemblies { get; private set; } = [];

    public void RegisterMediatRFromAssemblies(params Assembly[] assemblies) => MediatRAssemblies = assemblies ?? [];

}
