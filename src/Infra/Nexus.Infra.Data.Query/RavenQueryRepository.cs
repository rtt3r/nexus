using Goal.Infra.Crosscutting.Collections;
using Goal.Infra.Data.Raven;
using Goal.Infra.Data.Query;
using Raven.Client.Documents;
using Raven.Client.Documents.Session;

namespace Nexus.Infra.Data.Query;

public abstract class RavenQueryRepository<TEntity>(IAsyncDocumentSession dbSession) : QueryRepository<TEntity, string>
    where TEntity : class
{
    private bool disposed;

    protected IAsyncDocumentSession dbSession = dbSession;

    public override async Task<TEntity> LoadAsync(string id, CancellationToken cancellationToken = new CancellationToken())
    {
        return await dbSession
            .LoadAsync<TEntity>(id, cancellationToken);
    }

    public override async Task<ICollection<TEntity>> QueryAsync(CancellationToken cancellationToken = new CancellationToken())
    {
        return await dbSession
            .Query<TEntity>()
            .ToListAsync(cancellationToken);
    }

    public override async Task<IPagedList<TEntity>> QueryAsync(IPageSearch pageSearch, CancellationToken cancellationToken = new CancellationToken())
    {
        return await dbSession
            .Query<TEntity>()
            .ToPagedListAsync(pageSearch, cancellationToken);
    }

    public override async Task StoreAsync(string id, TEntity entity, CancellationToken cancellationToken = new CancellationToken())
    {
        await dbSession.StoreAsync(entity, id, cancellationToken);
        await dbSession.SaveChangesAsync(cancellationToken);
    }

    public override async Task RemoveAsync(TEntity entity, CancellationToken cancellationToken = new CancellationToken())
    {
        dbSession.Delete(entity);
        await dbSession.SaveChangesAsync(cancellationToken);
    }

    public override async Task RemoveAsync(string id, CancellationToken cancellationToken = new CancellationToken())
    {
        dbSession.Delete(id);
        await dbSession.SaveChangesAsync(cancellationToken);
    }

    protected override void Dispose(bool disposing)
    {
        if (!disposed)
        {
            if (disposing)
            {
                dbSession.Dispose();
            }

            disposed = true;
        }
    }
}
