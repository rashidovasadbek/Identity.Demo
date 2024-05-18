using Identity.Domain.Common.Commands;
using Identity.Domain.Entities;
using Identity.Persistence.Caching.Brokers;
using Identity.Persistence.Caching.Models;
using Identity.Persistence.Repositories.Interfaces;

namespace Identity.Persistence.Repositories;

public class RefreshTokenRepository(ICacheBroker cacheBroker) : IRefreshTokenRepository
{
    public async ValueTask<RefreshToken> CreateAsync(RefreshToken refreshToken, CommandOptions commandOptions = default,
        CancellationToken cancellationToken = default)
    {
        var cacheEntryOptions = new CacheEntryOptions(null, refreshToken.ExpiryTime - DateTimeOffset.UtcNow);
        await cacheBroker.SetAsync($"{nameof(RefreshToken)} - {(refreshToken.Token)}", refreshToken, cacheEntryOptions, cancellationToken);

        return refreshToken;
    }

    public async ValueTask<RefreshToken?> GetByValueAsync(string refreshTokenValue, CancellationToken cancellationToken = default)
        => await cacheBroker.GetAsync<RefreshToken>($"{nameof(RefreshToken)} - {(refreshTokenValue)}", cancellationToken);

    public async ValueTask RemoveAsync(string refreshTokenValue, CancellationToken cancellationToken = default)
        => await cacheBroker.DeleteAsync($"{nameof(RefreshToken)} - {(refreshTokenValue)}", cancellationToken);
}