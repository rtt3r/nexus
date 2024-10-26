using Goal.Infra.Crosscutting.Collections;
using Goal.Infra.Crosscutting.Extensions;
using Goal.Infra.Crosscutting.Specifications;
using Goal.Infra.Data;
using Goal.Infra.Data.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using Nexus.Core.Domain.Users.Aggregates;

namespace Nexus.Core.Infra.Data.Repositories;

public class UserRepository(CoreDbContext context) : Repository<User, string>(context), IUserRepository
{
    public override User? Get(string key)
    {
        return Context
            .Set<User>()
            .Include(x => x.Emails)
            .FirstOrDefault(x => x.Id == key);
    }

    public override ICollection<User> List()
        => [.. Context.Set<User>().Include(x => x.Emails)];

    public override ICollection<User> Search(ISpecification<User> specification)
        => [.. FindSpecific(specification)];

    public override IPagedList<User> Search(ISpecification<User> specification, IPageSearch pageSearch)
    {
        ArgumentNullException.ThrowIfNull(pageSearch, nameof(pageSearch));
        return FindSpecific(specification).ToPagedList(pageSearch);
    }

    public override IPagedList<User> Search(IPageSearch pageSearch)
        => Search(new TrueSpecification<User>(), pageSearch);

    public override async Task<User?> GetAsync(string key, CancellationToken cancellationToken = default)
    {
        cancellationToken.ThrowIfCancellationRequested();

        return await Context
            .Set<User>()
            .Include(x => x.Emails)
            .FirstOrDefaultAsync(x => x.Id == key, cancellationToken);
    }

    public override async Task<ICollection<User>> ListAsync(CancellationToken cancellationToken = default)
    {
        return await Context
            .Set<User>()
            .Include(x => x.Emails)
            .AsNoTracking()
            .ToListAsync(cancellationToken);
    }

    public override async Task<ICollection<User>> SearchAsync(ISpecification<User> specification, CancellationToken cancellationToken = default)
        => await FindSpecific(specification).ToListAsync(cancellationToken);

    public override async Task<IPagedList<User>> SearchAsync(IPageSearch pageSearch, CancellationToken cancellationToken = default)
        => await SearchAsync(new TrueSpecification<User>(), pageSearch, cancellationToken);

    public override async Task<IPagedList<User>> SearchAsync(ISpecification<User> specification, IPageSearch pageSearch, CancellationToken cancellationToken = default)
    {
        ArgumentNullException.ThrowIfNull(pageSearch, nameof(pageSearch));
        return await FindSpecific(specification).ToPagedListAsync(pageSearch, cancellationToken);
    }

    private IQueryable<User> FindSpecific(ISpecification<User> specification)
    {
        ArgumentNullException.ThrowIfNull(specification, nameof(specification));

        return Context
            .Set<User>()
            .Include(x => x.Emails)
            .AsNoTracking()
            .Where(specification.SatisfiedBy());
    }
}
