using Identity.Domain.Common.Commands;
using Identity.Domain.Entities;

namespace Identity.Persistence.Repositories.Interfaces;

/// <summary>
/// Interface for accessing and managing RefreshToken entities in a data store.
/// </summary>
public interface IRefreshTokenRepository
{
    /// <summary>
    /// Asynchronously creates a new RefreshToken entity.
    /// </summary>
    /// <param name="refreshToken">The RefreshToken entity to be created</param>
    /// <param name="commandOptions"></param>
    /// <param name="cancellationToken">Cancellation token to stop the operation if needed (default is none)</param>
    /// <returns>A ValueTask representing the asynchronous operation, returning the created RefreshToken</returns>
    ValueTask<RefreshToken> CreateAsync(
        RefreshToken refreshToken,
        CommandOptions commandOptions = default,
        CancellationToken cancellationToken = default);

    /// <summary>
    /// 
    /// </summary>
    /// <param name="refreshTokenValue"></param>
    /// <param name="cancellationToken"></param>
    /// <returns></returns>
    ValueTask<RefreshToken?> GetByValueAsync(
        string refreshTokenValue,
        CancellationToken cancellationToken = default);

    /// <summary>
    /// 
    /// </summary>
    /// <param name="refreshTokenValue"></param>
    /// <param name="cancellationToken"></param>
    /// <returns></returns>
    ValueTask RemoveAsync(
        string refreshTokenValue,
        CancellationToken cancellationToken = default);
}