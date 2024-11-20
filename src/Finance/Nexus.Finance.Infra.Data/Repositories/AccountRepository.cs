using Goal.Infra.Crosscutting.Collections;
using Goal.Infra.Crosscutting.Extensions;
using Goal.Infra.Crosscutting.Specifications;
using Goal.Infra.Data;
using Goal.Infra.Data.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using Nexus.Finance.Domain.Accounts.Aggregates;
using Nexus.Infra.Crosscutting;

namespace Nexus.Finance.Infra.Data.Repositories;

internal sealed class AccountRepository(FinanceDbContext context, AppState appState)
    : Repository<Account, string>(context), IAccountRepository
{
    private readonly AppState appState = appState;

    public override Account? Get(string key)
    {
        return GetBaseQuery()
            .FirstOrDefault(p => p.Id == key);
    }

    public override Task<Account?> GetAsync(string key, CancellationToken cancellationToken = default)
    {
        return GetBaseQuery()
            .FirstOrDefaultAsync(p => p.Id == key, cancellationToken);
    }

    public override ICollection<Account> List()
        => [.. GetBaseQuery()];

    public override async Task<ICollection<Account>> ListAsync(CancellationToken cancellationToken = default)
    {
        return await GetBaseQuery()
            .ToListAsync(cancellationToken);
    }

    public override IPagedList<Account> Search(IPageSearch pageSearch)
    {
        ArgumentNullException.ThrowIfNull(pageSearch, nameof(pageSearch));

        return GetBaseQuery()
            .ToPagedList(pageSearch);
    }

    public override ICollection<Account> Search(ISpecification<Account> specification)
    {
        ArgumentNullException.ThrowIfNull(specification, nameof(specification));
        return [.. GetBaseQuery().Where(specification.SatisfiedBy())];
    }

    public override IPagedList<Account> Search(ISpecification<Account> specification, IPageSearch pageSearch)
    {
        ArgumentNullException.ThrowIfNull(pageSearch, nameof(pageSearch));

        return GetBaseQuery()
            .Where(specification.SatisfiedBy())
            .ToPagedList(pageSearch);
    }

    public override async Task<IPagedList<Account>> SearchAsync(IPageSearch pageSearch, CancellationToken cancellationToken = default)
    {
        ArgumentNullException.ThrowIfNull(pageSearch, nameof(pageSearch));

        return await GetBaseQuery()
            .ToPagedListAsync(pageSearch, cancellationToken);
    }

    public override async Task<ICollection<Account>> SearchAsync(ISpecification<Account> specification, CancellationToken cancellationToken = default)
    {
        ArgumentNullException.ThrowIfNull(specification, nameof(specification));

        return await GetBaseQuery()
            .Where(specification.SatisfiedBy())
            .ToListAsync(cancellationToken);
    }

    public override Task<IPagedList<Account>> SearchAsync(ISpecification<Account> specification, IPageSearch pageSearch, CancellationToken cancellationToken = default)
    {
        ArgumentNullException.ThrowIfNull(pageSearch, nameof(pageSearch));

        return GetBaseQuery()
            .Where(specification.SatisfiedBy())
            .ToPagedListAsync(pageSearch, cancellationToken);
    }

    public override bool Any()
    {
        return GetBaseQuery()
            .AsNoTracking()
            .Any();
    }

    public override bool Any(ISpecification<Account> specification)
    {
        return GetBaseQuery()
            .AsNoTracking()
            .Where(specification.SatisfiedBy())
            .Any();
    }

    public override async Task<bool> AnyAsync(CancellationToken cancellationToken = default)
    {
        return await GetBaseQuery()
            .AsNoTracking()
            .AnyAsync(cancellationToken);
    }

    public override async Task<bool> AnyAsync(ISpecification<Account> specification, CancellationToken cancellationToken = default)
    {
        return await GetBaseQuery()
            .AsNoTracking()
            .Where(specification.SatisfiedBy())
            .AnyAsync(cancellationToken);
    }

    public async Task<Account?> GetByName(string name, CancellationToken cancellationToken = default)
    {
        return await GetBaseQuery()
            .FirstOrDefaultAsync(
                p => p.Name.Equals(name, StringComparison.CurrentCultureIgnoreCase),
                cancellationToken);
    }

    private IQueryable<Account> GetBaseQuery()
    {
        return Context
            .Set<Account>()
            .Where(p => p.UserId.Equals(appState.User!.UserId, StringComparison.CurrentCultureIgnoreCase));
    }
}
