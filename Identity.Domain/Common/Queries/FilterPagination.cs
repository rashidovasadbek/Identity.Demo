namespace Identity.Domain.Common.Queries;

/// <summary>
/// Represents a pagination filter with  page size and page token  for querying results a collection of items.
/// </summary>
public class FilterPagination
{
    /// <summary>
    /// Gets or sets the number of items to include on the each page.(default 20)
    /// </summary>
    public uint PageSize { get; set; } = 20;

    /// <summary>
    /// Gets or sets the token representing the page to retrieve in a paginated collection. (default 1)
    /// </summary>
    public uint PageToken { get; set; } = 1;

    /// <summary>
    /// Initializes a new instance of the <see cref="FilterPagination"/> class specified  the page size and token 
    /// </summary>
    /// <param name="pageSize"></param>
    /// <param name="pageToken"></param>
    public FilterPagination(uint pageSize, uint pageToken)
    {
        PageSize = pageSize;
        PageToken = pageToken;
    }

    public FilterPagination()
    {
        
    }

    /// <summary>
    /// Get the hash code for this current instance
    /// </summary>
    /// <returns></returns>
    public override int GetHashCode()
    {
        var hashCode = new HashCode();

        hashCode.Add(PageSize);
        hashCode.Add(PageToken);

        return hashCode.ToHashCode();
    }

    /// <summary>
    /// Determine whether the specified object is equal to the current instance
    /// </summary>
    /// <param name="obj"></param>
    /// <returns></returns>
    public override bool Equals(object obj)
    {
        return obj is FilterPagination filterPagination && filterPagination.GetHashCode() == GetHashCode();
    }

}