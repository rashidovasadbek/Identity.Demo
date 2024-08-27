using Force.DeepCloner;
using Identity.Application.Common.Settings;
using Identity.Persistence.Caching.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Caching.Memory;
using Microsoft.Extensions.Options;


namespace Identity.Infrastructure.Caching;


public class DefaultMemoryCacheBroker(IOptions<CacheSettings> cacheSettings, IMemoryCache memoryCache)
{
    private readonly MemoryCacheEntryOptions _memoryCacheEntryOptions = new()
    {
        AbsoluteExpirationRelativeToNow = TimeSpan.FromSeconds(cacheSettings.Value.AbsoluteExpirationInSeconds),
        SlidingExpiration = TimeSpan.FromSeconds(cacheSettings.Value.SlidingExpirationInSeconds)
    };

    public ValueTask<T?> GetAsync<T>(string key, CancellationToken cancellationToken = default)
    {
        var value = memoryCache.Get<T?>(key);
        
        return ValueTask.FromResult(value);
    }

    public ValueTask<bool> TryGetAsync<T>(string key, out T? value, CancellationToken cancellationToken = default)
    {
        value =  memoryCache.Get<T?>(key);

        return value is not null ? ValueTask.FromResult(true) : ValueTask.FromResult(false);
    }

    public async ValueTask<T?> GetOrSetAsync<T>(
        string key,
        T value,
        CacheEntryOptions? entryOptions = default,
        CancellationToken cancellationToken = default
    )
    {
        var cachedValue = memoryCache.Get<T?>(key);
        
        if(cachedValue  is not null)
            return cachedValue;
        
        await SetAsync(key, value, entryOptions, cancellationToken);
        
        return value;
    }

    public async ValueTask<T?> GetOrSetAsync<T>(
        string key,
        Func<T> valueProvider,
        CacheEntryOptions? entryOptions = default,
        CancellationToken cancellationToken = default)
    {
        var cachedValue = memoryCache.Get<T?>(key);
        
        if(cachedValue  is not null)
            return cachedValue;
        
        var value = valueProvider();
        await SetAsync(key, () => value, entryOptions, cancellationToken);
        
        return value;
    }

    public async ValueTask<T?> GetOrSetAsync<T>(
        string key,
        Func<ValueTask<T>> valueProvider,
        CacheEntryOptions? entryOptions = default,
        CancellationToken cancellationToken = default)
    {
        var cachedValue = memoryCache.Get<T?>(key);
        
        if(cachedValue  is not null)
            return cachedValue;
        
        var value = await valueProvider();
        await SetAsync(key, () => value, entryOptions, cancellationToken);
        
        return value;
    }

    public ValueTask SetAsync<T>(
        string key,
        T value,
        CacheEntryOptions? entryOptions = default,
        CancellationToken cancellationToken = default)
    {
        memoryCache.Set(key, value, GetCacheEntryOptions(entryOptions));

        return ValueTask.CompletedTask;
    }

    public ValueTask SetAsync<T>(
        string key,
        Func<T> valueProvider,
        CacheEntryOptions? entryOptions,
        CancellationToken cancellationToken = default)
    {
        memoryCache.Set(key, valueProvider, GetCacheEntryOptions(entryOptions));
        return ValueTask.CompletedTask;
    }

    public async ValueTask SetAsync<T>(
        string key,
        Func<ValueTask<T>> valueProvider,
        CacheEntryOptions? entryOptions,
        CancellationToken cancellationToken = default)
    {
        memoryCache.Set(key, await valueProvider(), GetCacheEntryOptions(entryOptions));
    }

    private MemoryCacheEntryOptions GetCacheEntryOptions(CacheEntryOptions? entryOptions)
    {
        if(!entryOptions.HasValue ||
           (!entryOptions.Value.AbsoluteExpressionRelativeToNow.HasValue && !entryOptions.Value.SlidingExpiration.HasValue))
            return _memoryCacheEntryOptions;

        var currentEntryOptions = _memoryCacheEntryOptions.DeepClone();
        currentEntryOptions.AbsoluteExpirationRelativeToNow = entryOptions.Value.AbsoluteExpressionRelativeToNow;
        currentEntryOptions.SlidingExpiration = entryOptions.Value.SlidingExpiration;
        
        return currentEntryOptions;
    }
}