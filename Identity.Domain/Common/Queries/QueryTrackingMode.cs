namespace Identity.Domain.Common.Queries;

/// <summary>
/// Represents  different query tracking modes that can be used when query
/// </summary>
public enum QueryTrackingMode
{
    /// <summary>
    /// Specifies that queries should track changes to entities.
    /// </summary>
    AsTracking,
    
    /// <summary>
    /// Specifies that queries should not track changes to entities
    /// </summary>
    AsNoTracking,
    
    /// <summary>
    /// Specifies that queries should not track changes to entities, but identify resolutions is still performed
    /// </summary>
    AsNoTrackingWithIdentityResolution
}