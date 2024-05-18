namespace Identity.Application.Common.Settings;

/// <summary>
/// Represents the configuration settings for caching in a system 
/// </summary>
public class CacheSettings
{
    /// <summary>
    /// Gets or sets absolute expiration time in seconds, measured in seconds
    /// </summary>
    public uint AbsoluteExpirationInSeconds { get; set; }
    
    /// <summary>
    /// Gets or sets sliding expiration time in seconds, measured in seconds
    /// </summary>
    public uint SlidingExpirationInSeconds { get; set; }
}