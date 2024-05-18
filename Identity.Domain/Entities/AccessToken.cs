using Identity.Domain.Common.Entities;

namespace Identity.Domain.Entities;

public class AccessToken : Entity
{
    /// <summary>
    /// Default constructor.
    /// </summary>
    public AccessToken()
    {
        
    }
    
    /// <summary>
    /// Parameterized constructor to initialize an access token.
    /// </summary>
    /// <param name="id">The unique identifier of the access token.</param>
    /// <param name="userId">The unique identifier of the associated user.</param>
    /// <param name="token">The access token string.</param>
    /// <param name="expiryTime">The expiration time of the access token.</param>
    /// <param name="isRevoked">A flag indicating whether the access token is revoked.</param>
    public AccessToken(Guid id, Guid userId, string token, DateTimeOffset expiryTime, bool isRevoked)
    {
        Id = id;
        UserId = userId;
        Token = token;
        ExpiryTime = expiryTime;
        IsRevoked = isRevoked;
    }

    /// <summary>
    /// Gets or set the unique identifier of associated the user
    /// </summary>
    public Guid UserId { get; set; }
    
    /// <summary>
    /// Gets or set the access token
    /// </summary>
    public string Token { get; set; } = default!;
    
    /// <summary>
    /// Gets or  set the expiration time of  access token
    /// </summary>
    public DateTimeOffset ExpiryTime { get; set; } = default;
    
    /// <summary>
    /// Gets or set a flag indicating whether the access token is revoked
    /// </summary>
    public bool IsRevoked { get; set; } = default;
}