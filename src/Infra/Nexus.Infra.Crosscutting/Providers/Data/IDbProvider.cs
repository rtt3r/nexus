using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace Nexus.Infra.Crosscutting.Providers.Data;

public interface IDbProvider
{
    IServiceCollection Configure(IServiceCollection services, IConfiguration configuration);
}
