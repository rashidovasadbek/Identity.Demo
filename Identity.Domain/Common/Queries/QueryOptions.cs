namespace Identity.Domain.Common.Queries;

/// <summary>
/// Represents options for querying entities, including tracking mode.
/// </summary>
public struct QueryOptions
{
    /// <summary>
    /// Gets or sets the tracking mode for the query.
    /// </summary>
    public QueryTrackingMode TrackingMode { get; set; }

    /// <summary>
    /// Initializes a new instance of the <see cref="QueryOptions"/> struct with the specified tracking mode.
    /// </summary>
    /// <param name="trackingMode">The tracking mode for the query.</param>
    public QueryOptions(QueryTrackingMode trackingMode) : this() => TrackingMode = trackingMode;
}
