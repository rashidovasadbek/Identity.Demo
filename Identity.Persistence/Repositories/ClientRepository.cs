using System.Linq.Expressions;
using Identity.Domain.Common.Commands;
using Identity.Domain.Common.Queries;
using Identity.Domain.Entities;
using Identity.Persistence.Caching.Brokers;
using Identity.Persistence.DataContexts;
using Identity.Persistence.Repositories.Interfaces;

namespace Identity.Persistence.Repositories;

/// <summary>
/// Provides client repositories functionality
/// </summary>
/// <param name="dbContext"></param>
public class ClientRepository(AppDbContext dbContext, ICacheBroker cacheBroker) : EntityRepositoryBase<Client, AppDbContext>(dbContext, cacheBroker), IClientRepository
{
    public new IQueryable<Client> Get(Expression<Func<Client, bool>>? predicate = default, QueryOptions queryOptions = default)
        => base.Get(predicate, queryOptions);

    public new ValueTask<Client> GetByIdAsync(Guid clientId, QueryOptions queryOptions = default, CancellationToken cancellationToken = default)
        => base.GetByIdAsync(clientId, queryOptions, cancellationToken);

    public new ValueTask<Client> CreateAsync(Client client, CommandOptions commandOptions = default, CancellationToken cancellationToken = default)
        => base.CreateAsync(client, commandOptions, cancellationToken);

    public new ValueTask<Client> UpdateAsync(Client client, CommandOptions commandOptions = default, CancellationToken cancellationToken = default)
        => base.UpdateAsync(client,commandOptions, cancellationToken);

    public new ValueTask<Client?> DeleteByIdAsync(Guid clientId, CommandOptions commandOptions = default, CancellationToken cancellationToken = default)
        => base.DeleteByIdAsync(clientId, commandOptions, cancellationToken);

    public new ValueTask<Client?> DeleteAsync(Client client, CommandOptions commandOptions = default, CancellationToken cancellationToken = default)
        => base.DeleteAsync(client, commandOptions, cancellationToken);
}