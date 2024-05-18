using Identity.Domain.Common.Entities;

namespace Identity.Domain.Entities;

/// <summary>
/// Represents the Refresh token
/// </summary>
public class RefreshToken : Entity
{
    /// <summary>
    /// Gets or sets the unique identifier of the associated user
    /// </summary>
    public Guid UserId { get; set; }

    /// <summary>
    /// Gets or sets the refresh token string 
    /// </summary>
    public string Token { get; set; } = default!;

    /// <summary>
    /// Gets or sets the expiration time of the refresh token
    /// </summary>
    public DateTimeOffset ExpiryTime { get; set; } = default!;
    
    /// <summary>
    /// Gets or sets the if it is  enable to extend expiration time of the refresh token
    /// </summary>
    public bool  EnableExtendedExpiryTime { get; set; } = default!;
}