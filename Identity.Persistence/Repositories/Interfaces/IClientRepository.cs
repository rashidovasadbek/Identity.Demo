using System.Linq.Expressions;
using Identity.Domain.Common.Commands;
using Identity.Domain.Common.Queries;
using Identity.Domain.Entities;

namespace Identity.Persistence.Repositories.Interfaces;

/// <summary>
/// Represents a repository for managing client entities
/// </summary>
public interface IClientRepository
{
    /// <summary>
    /// Gets queryable source of clients  based on an optional predicate  and query options
    /// </summary>
    /// <param name="predicate">optional predicate to filter clients</param>
    /// <param name="queryOptions">Query options </param>
    /// <returns>Query source of clients</returns>
    IQueryable<Client> Get(Expression<Func<Client, bool>>? predicate = default, QueryOptions queryOptions = default);
    
    /// <summary>
    /// Gets a single client by their identifier
    /// </summary>
    /// <param name="clientId">The unique identifier of the user</param>
    /// <param name="queryOptions">Query options for sorting, paging, etc</param>
    /// <param name="cancellationToken">Cancellation token</param>
    /// <returns>Client if found, otherwise null</returns>
    ValueTask<Client> GetByIdAsync(Guid clientId, QueryOptions queryOptions = default, CancellationToken cancellationToken = default);
    
    /// <summary>
    /// Creates a client
    /// </summary>
    /// <param name="client">The user to be created</param>
    /// <param name="commandOptions">Create command options</param>
    /// <param name="cancellationToken">Cancellation token</param>
    /// <returns>Created user</returns>
    ValueTask<Client> CreateAsync(Client client, CommandOptions commandOptions = default, CancellationToken cancellationToken = default);
    
    /// <summary>
    /// Update an existing client
    /// </summary>
    /// <param name="client">The client to be updated</param>
    /// <param name="commandOptions">Update command options</param>
    /// <param name="cancellationToken">Cancellation token</param>
    /// <returns>Updated client</returns>
    ValueTask<Client> UpdateAsync(Client client, CommandOptions commandOptions = default, CancellationToken cancellationToken = default);
    
    /// <summary>
    /// Delete a client by their ID   
    /// </summary>
    /// <param name="clientId">The id of the client to be  deleted</param>
    /// <param name="commandOptions">deleted command options</param>
    /// <param name="cancellationToken">Cancellation token </param>
    /// <returns>Update client if soft  deleted, otherwise null</returns>
    ValueTask<Client?> DeleteByIdAsync(Guid clientId, CommandOptions commandOptions = default, CancellationToken cancellationToken = default);

    /// <summary>
    /// Delete a client  
    /// </summary>
    /// <param name="client">The  client to be  deleted</param>
    /// <param name="commandOptions">deleted command options</param>
    /// <param name="cancellationToken">Cancellation token </param>
    /// <returns>Update client if soft  deleted, otherwise null</returns>
    ValueTask<Client?> DeleteAsync(Client client, CommandOptions commandOptions = default, CancellationToken cancellationToken = default);
}
