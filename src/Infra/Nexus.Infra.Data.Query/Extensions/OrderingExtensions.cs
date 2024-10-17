using Goal.Infra.Crosscutting.Collections;
using Raven.Client.Documents.Linq;
using GoalQueryable = Goal.Infra.Crosscutting.Collections.Queryable;

namespace Nexus.Infra.Data.Query.Extensions;

public static class OrderingExtensions
{
    public static IRavenQueryable<T> OrderBy<T>(this IRavenQueryable<T> source, string fieldName, SortDirection direction)
        => GoalQueryable.Order<IRavenQueryable<T>, T>(source, fieldName, direction, false);

    public static IRavenQueryable<T> OrderBy<T>(this IRavenQueryable<T> source, string fieldName)
        => GoalQueryable.Order<IRavenQueryable<T>, T>(source, fieldName, SortDirection.Asc, false);

    public static IRavenQueryable<T> OrderByDescending<T>(this IRavenQueryable<T> source, string fieldName)
        => GoalQueryable.Order<IRavenQueryable<T>, T>(source, fieldName, SortDirection.Desc, false);

    public static IRavenQueryable<T> ThenBy<T>(this IRavenQueryable<T> source, string fieldName, SortDirection direction)
        => GoalQueryable.Order<IRavenQueryable<T>, T>(source, fieldName, direction, true);

    public static IRavenQueryable<T> ThenBy<T>(this IRavenQueryable<T> source, string fieldName)
        => GoalQueryable.Order<IRavenQueryable<T>, T>(source, fieldName, SortDirection.Asc, true);

    public static IRavenQueryable<T> ThenByDescending<T>(this IRavenQueryable<T> source, string fieldName)
        => GoalQueryable.Order<IRavenQueryable<T>, T>(source, fieldName, SortDirection.Desc, true);
}
