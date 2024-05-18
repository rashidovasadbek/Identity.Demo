using Identity.Persistence.Caching.Models;

namespace Identity.Persistence.Caching.Brokers;

/// <summary>
/// Defines cache broker functionality
/// </summary>
public interface ICacheBroker
{
    /// <summary>
    /// Get the cache entry value with the specified key
    /// </summary>
    /// <param name="key">The key of the cache entry</param>
    /// <param name="cancellationToken">A cancellation  token to cancel the operation </param>
    /// <typeparam name="T">The type of the cache entry value</typeparam>
    /// <returns>Found instance of T, Otherwise default value</returns>
    ValueTask<T?> GetAsync<T>(string key, CancellationToken cancellationToken = default);
    
    /// <summary>
    /// Attempts to get cache entry with the specified key
    /// </summary>
    /// <param name="key">The key of cache entry value</param>
    /// <param name="value">Cached entry  value if found, otherwise default value </param>
    /// <param name="cancellationToken">A cancellation token to cancel the operation </param>
    /// <typeparam name="T">The type of the cache entry value</typeparam>
    /// <returns>True if the  cached  entry was found and retrieved successfully; otherwise, false</returns>
    ValueTask<bool> TryGetAsync<T>(string key, out T? value , CancellationToken cancellationToken = default);
    
    
    /// <summary>
    /// Attempts to get the cache entry with the specified key, if not found sets the cache entry with the specified key and value
    /// </summary>
    /// <param name="key">The key of cache entry value</param>
    /// <param name="value">Cached entry value if found, otherwise default value </param>
    /// <param name="entryOptions">A cache entry options to specify caching options</param>
    /// <param name="cancellationToken">A cancellation token to cancel the operation </param>
    /// <typeparam name="T"></typeparam>
    /// <returns>Found instance of <see cref="T"/> if found, otherwise default value.</returns>
    ValueTask<T?> GetOrSetAsync<T>(string key, T value, CacheEntryOptions? entryOptions = default, CancellationToken cancellationToken = default);
    
    /// <summary>
    /// Attempts to get the cache entry for the specified key, if not found set cache entry with the specified key and value
    /// </summary>
    /// <param name="key">The type of the cache entry value</param>
    /// <param name="valueProvider">A broker that provides the entry to set</param>
    /// <param name="entryOptions">A cache entry options to specify caching options</param>
    /// <param name="cancellationToken">A cancellation token to cancel the operation</param>
    /// <typeparam name="T">The type of the cache entry value</typeparam>
    /// <returns>Found instance of <see cref="T"/> if found, otherwise default value.</returns>
    ValueTask<T?> GetOrSetAsync<T>(string key, Func<T> valueProvider, CacheEntryOptions? entryOptions = default, CancellationToken cancellationToken = default);
    
    /// <summary>
    /// Attempt to get the cache entry for the specified key, if not found set cache entry with the specified key and value
    /// </summary>
    /// <param name="key">The type of the cache cache entry value</param>
    /// <param name="valueProvider">A broker that provides the entry to set</param>
    /// <param name="entryOptions">A cache entry options to specify caching options</param>
    /// <param name="cancellationToken">A cancellation token to cancel the operation</param>
    /// <typeparam name="T">The type of the cache entry value</typeparam>
    /// <returns>Found instance of <see cref="T"/> if found, otherwise default value</returns>
    ValueTask<T?> GetOrSetAsync<T>(string key, Func<ValueTask<T>> valueProvider, CacheEntryOptions? entryOptions = default, CancellationToken cancellationToken = default);
    
    /// <summary>
    /// Sets the value entry with the specified key and value provider
    /// </summary>
    /// <param name="key">The key of the cache entry value</param>
    /// <param name="valueProvider">Value to create cache entry for</param>
    /// <param name="entryOptions">A cache entry options to specify caching options</param>
    /// <param name="cancellationToken">A cancellation token  to cansel the operation</param>
    /// <typeparam name="T"></typeparam>
    /// <returns>Found instance of <see cref="T"/> if found, otherwise default value.</returns>
    ValueTask<T> SetAsync<T>(string key, T valueProvider, CacheEntryOptions? entryOptions = default, CancellationToken cancellationToken = default);
    
    /// <summary>
    /// Sets the value entry with the specified key and value provider
    /// </summary>
    /// <param name="key">The key of the cache entry value</param>
    /// <param name="valueProvider">Value to create cache entry for</param>
    /// <param name="entryOptions">A cache entry options to specify caching options</param>
    /// <param name="cancellationToken">A cancellation token  to cansel the operation</param>
    /// <typeparam name="T"></typeparam>
    /// <returns>Found instance of <see cref="T"/> if found, otherwise default value.</returns>
    ValueTask<T> SetAsync<T>(string key, Func<T> valueProvider, CacheEntryOptions? entryOptions = default, CancellationToken cancellationToken = default);
    
    /// <summary>
    /// Sets the value entry with the specified key and value provider
    /// </summary>
    /// <param name="key">The key of the cache entry value</param>
    /// <param name="valueProvider">Value to create cache entry for</param>
    /// <param name="entryOptions">A cache entry options to specify caching options</param>
    /// <param name="cancellationToken">A cancellation token  to cansel the operation</param>
    /// <typeparam name="T"></typeparam>
    /// <returns>Found instance of <see cref="T"/> if found, otherwise default value.</returns>
    ValueTask<T> SetAsync<T>(string key, Func<ValueTask<T>> valueProvider, CacheEntryOptions? entryOptions = default, CancellationToken cancellationToken = default);
    
    /// <summary>
    /// Delete the cache entry with the specified key
    /// </summary>
    /// <param name="key">The key of the cache entry.</param>
    /// <param name="cancellationToken">A cancellation token to cancel the operation.</param>
    /// <typeparam name="T">A <see cref="ValueTask"/> representing the asynchronous operation.</typeparam>
    /// <returns></returns>
    ValueTask DeleteAsync(string key, CancellationToken cancellationToken = default);
}