using Microsoft.AspNetCore.Builder;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Nexus.Core.Infra.Data;
using Nexus.Core.Infra.Data.EventSourcing;

namespace Nexus.Core.Infra.IoC.Extensions;

public static class ApplicationBuilderExtensionMethods
{
    public static WebApplication MigrateApiDbContext(this WebApplication app)
    {
        using (IServiceScope scope = app.Services.GetRequiredService<IServiceScopeFactory>().CreateScope())
        {
            scope.ServiceProvider.GetRequiredService<CoreDbContext>().Database.Migrate();
        }

        return app;
    }

    public static WebApplication MigrateWorkerDbContext(this WebApplication app)
    {
        using (IServiceScope scope = app.Services.GetRequiredService<IServiceScopeFactory>().CreateScope())
        {
            scope.ServiceProvider.GetRequiredService<EventSourcingDbContext>().Database.Migrate();
        }

        return app;
    }
}
