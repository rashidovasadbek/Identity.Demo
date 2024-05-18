using System.Linq.Expressions;
using Identity.Domain.Common.Commands;
using Identity.Domain.Common.Entities;
using Identity.Domain.Common.Queries;
using Identity.Persistence.Caching.Brokers;
using Identity.Persistence.Caching.Models;
using Identity.Persistence.Extensions;
using Microsoft.EntityFrameworkCore;

namespace Identity.Persistence.Repositories;

/// <summary>
/// Represents a base class for entities with common CRUD operations
/// </summary>
/// <param name="dbContext"></param>
/// <param name="cacheBroker"></param>
/// <param name="cacheEntryOptions"></param>
/// <typeparam name="TEntity"></typeparam>
/// <typeparam name="TContext"></typeparam>
public abstract class EntityRepositoryBase<TEntity, TContext>
(
    TContext dbContext,
    ICacheBroker cacheBroker,
    CacheEntryOptions? cacheEntryOptions = default)
    where TEntity : class, IEntity where TContext : DbContext
{
    protected TContext DbContext => dbContext;

    /// <summary>
    /// Retrieves entities from the repository based on options filtering condition and tracking preferences.
    /// </summary>
    /// <param name="predicate"></param>
    /// <param name="asNoTracking"></param>
    /// <returns></returns>
    protected IQueryable<TEntity> Get(Expression<Func<TEntity, bool>>? predicate = default, bool asNoTracking = false)
    {
        var initialQuery = dbContext.Set<TEntity>().Where(entity => true);
        
        if (predicate is not null)
            initialQuery = initialQuery.Where(predicate);
        
        if (asNoTracking)
            initialQuery = initialQuery.AsNoTracking();
        
        return initialQuery;
    }

    /// <summary>
    /// Retrieves entities from the repository based on options filtering condition and tracking preferences.
    /// </summary>
    /// <param name="predicate"></param>
    /// <param name="options"></param>
    /// <returns></returns>
    protected IQueryable<TEntity> Get(Expression<Func<TEntity, bool>>? predicate = default, QueryOptions options = default)
    {
        var initialQuery = dbContext.Set<TEntity>().Where(entity => true);
        
        if(predicate is not null)
            initialQuery = initialQuery.Where(predicate);

        return initialQuery;
    }
    
    /// <summary>
    /// Asynchronously retrieves an entity from repository by its ID, optionally applying cashing.
    /// </summary>
    /// <param name="id"></param>
    /// <param name="queryOptions"></param>
    /// <param name="cancellationToken"></param>
    /// <returns></returns>
    protected async ValueTask<TEntity> GetByIdAsync(
        Guid id,
        QueryOptions queryOptions = default,
        CancellationToken cancellationToken = default)
    {
        var  foundEntity = default(TEntity?);
        if (cacheEntryOptions is null || !await cacheBroker.TryGetAsync<TEntity>(id.ToString(), out var cachedEntity, cancellationToken))
        {
            var initialQuery = DbContext.Set<TEntity>().AsQueryable();
            
            initialQuery.ApplyTrackingMode(queryOptions.TrackingMode);
            
            foundEntity = await initialQuery.FirstOrDefaultAsync(entity => entity.Id == id, cancellationToken);
            
            if (cacheEntryOptions is not null || foundEntity is not null)
                await cacheBroker.SetAsync(id.ToString(), foundEntity, cacheEntryOptions, cancellationToken);
        }
        else
            foundEntity = cachedEntity;
        
        return foundEntity;
    }

    /// <summary>
    /// Check if entity exists
    /// </summary>
    /// <param name="entityId"></param>
    /// <param name="cancellationToken"></param>
    /// <returns> True if entity exists, otherwise false </returns>
    protected async ValueTask<bool> CheckByIdAsync(Guid entityId, CancellationToken cancellationToken = default)
    {
        var foundEntity = await DbContext.Set<TEntity>()
            .Select(entity => entity.Id)
            .FirstOrDefaultAsync(foundEntityId => foundEntityId == entityId, cancellationToken);

        return foundEntity != Guid.Empty;
    }

    /// <summary>
    /// Asynchronously retrieves entities from repository  by collection of IDs
    /// </summary>
    /// <param name="ids"></param>
    /// <param name="queryOptions"></param>
    /// <param name="cancellationToken"></param>
    /// <returns></returns>
    protected async ValueTask<IList<TEntity>> GetByIdsAsync(
        IEnumerable<Guid> ids,
        QueryOptions queryOptions = default,
        CancellationToken cancellationToken = default)
    {
        var initialQuery = DbContext.Set<TEntity>().Where(entity => ids.Contains(entity.Id));
        
        initialQuery.ApplyTrackingMode(queryOptions.TrackingMode);

        return await initialQuery.ToListAsync(cancellationToken);
    }

    /// <summary>
    /// Asynchronously creates a new entity in the repository
    /// </summary>
    /// <param name="entity"></param>
    /// <param name="commandOptions"></param>
    /// <param name="cancellationToken"></param>
    /// <returns></returns>
    protected async ValueTask<TEntity> CreateAsync(
        TEntity entity,
        CommandOptions commandOptions = default,
        CancellationToken cancellationToken = default)
    {
        await DbContext.Set<TEntity>().AddAsync(entity, cancellationToken);

        if (cacheEntryOptions is not null)
            await cacheBroker.SetAsync(entity.Id.ToString(), entity, cacheEntryOptions, cancellationToken);

        if (!commandOptions.SkipSaveChanges)
            await DbContext.SaveChangesAsync(cancellationToken);
        
        return entity;
    }

    /// <summary>
    /// Asynchronously update a new entity in the  repository
    /// </summary>
    /// <param name="entity"></param>
    /// <param name="commandOptions"></param>
    /// <param name="cancellationToken"></param>
    /// <returns></returns>
    protected async ValueTask<TEntity> UpdateAsync(
        TEntity entity,
        CommandOptions commandOptions = default,
        CancellationToken cancellationToken = default)
    {
        DbContext.Set<TEntity>().Update(entity);

        if (cacheEntryOptions is not null)
            await cacheBroker.SetAsync(entity.Id.ToString(), entity, cacheEntryOptions, cancellationToken);

        if (commandOptions.SkipSaveChanges)
            await DbContext.SaveChangesAsync(cancellationToken);
        
        return entity;
    }

    /// <summary>
    /// Asynchronously deletes a new entity in the repository
    /// </summary>
    /// <param name="entity"></param>
    /// <param name="commandOptions"></param>
    /// <param name="cancellationToken"></param>
    /// <returns></returns>
    protected async ValueTask<TEntity?> DeleteAsync(
        TEntity entity,
        CommandOptions commandOptions = default,
        CancellationToken cancellationToken = default)
    {
        DbContext.Set<TEntity>().Remove(entity);

        if (cacheEntryOptions is not null)
            await cacheBroker.DeleteAsync(entity.Id.ToString(), cancellationToken);

        if (commandOptions.SkipSaveChanges)
            await DbContext.SaveChangesAsync(cancellationToken);
        
        return entity;
    }

    /// <summary>
    /// Asynchronously deletes an existing entity from the repository  by its ID
    /// </summary>
    /// <param name="id"></param>
    /// <param name="commandOptions"></param>
    /// <param name="cancellationToken"></param>
    /// <returns></returns>
    /// <exception cref="InvalidOperationException"></exception>
    protected async ValueTask<TEntity?> DeleteByIdAsync(
        Guid id,
        CommandOptions commandOptions = default,
        CancellationToken cancellationToken = default)
    {
        var entity = await DbContext
                         .Set<TEntity>()
                         .FirstOrDefaultAsync(entity => entity.Id == id, cancellationToken)
                     ?? throw new InvalidOperationException();
        
        DbContext.Remove(entity);
        
        if(cacheEntryOptions is not null)
            await cacheBroker.DeleteAsync(entity.Id.ToString(), cancellationToken);

        if (!commandOptions.SkipSaveChanges)
            await DbContext.SaveChangesAsync(cancellationToken);
        
        return entity;
    }
}