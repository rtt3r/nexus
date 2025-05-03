using Goal.Infra.Crosscutting.Collections;
using Raven.Client.Documents;
using Raven.Client.Documents.Linq;
using Raven.Client.Documents.Session;

namespace Nexus.Infra.Data.Query.Extensions;

public static class PaginationExtensions
{
    public static IRavenQueryable<T> Paginate<T>(this IRavenQueryable<T> source, IPageSearch pageSearch)
    {
        ArgumentNullException.ThrowIfNull(pageSearch);

        source = source.Skip(pageSearch.PageIndex * pageSearch.PageSize);
        source = source.Take(pageSearch.PageSize);

        return source;
    }

    public static IPagedList<T> ToPagedList<T>(this IRavenQueryable<T> source, IPageSearch pageSearch)
    {
        ArgumentNullException.ThrowIfNull(pageSearch);

        var data = source
            .Statistics(out QueryStatistics stats)
            .Paginate(pageSearch)
            .ToList();

        return new PagedList<T>(
            data,
            (int)stats.TotalResults);
    }

    public static async Task<IPagedList<T>> ToPagedListAsync<T>(this IRavenQueryable<T> source, IPageSearch pageSearch, CancellationToken cancellationToken = new CancellationToken())
    {
        ArgumentNullException.ThrowIfNull(pageSearch);

        List<T> data = await source
            .Statistics(out QueryStatistics stats)
            .Paginate(pageSearch)
            .ToListAsync(cancellationToken);

        return new PagedList<T>(
            data,
            (int)stats.TotalResults);
    }
}
