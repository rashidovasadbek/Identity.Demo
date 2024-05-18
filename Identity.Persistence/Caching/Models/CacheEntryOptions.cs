namespace Identity.Persistence.Caching.Models;

/// <summary>
/// Represents the options for configuring cache entry behavior
/// </summary>
/// <param name="absoluteExpressionRelativeToNow"></param>
/// <param name="slidingExpiration"></param>
public readonly struct CacheEntryOptions(TimeSpan? absoluteExpressionRelativeToNow, TimeSpan? slidingExpiration)
{
    /// <summary>
    /// Gets or sets the absolute expression relative to the current moment
    /// </summary>
    public TimeSpan? AbsoluteExpressionRelativeToNow { get; } = absoluteExpressionRelativeToNow;
    
    /// <summary>
    /// Gets or sets the sliding expression relative time for cached items
    /// </summary>
    public TimeSpan? SlidingExpiration { get; } = slidingExpiration;
}