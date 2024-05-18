using Identity.Domain.Common.Entities;
using Identity.Domain.Enums;

namespace Identity.Domain.Entities;

/// <summary>
/// Represents a user entity in the system
/// </summary>
public class Client : AuditableEntity
{
    /// <summary>
    /// Gets or sets the first name of the user
    /// </summary>
    public string FirstName { get; set; } = default!;
    
    /// <summary>
    /// Gets or sets the last name of the user
    /// </summary>
    public string LastName { get; set; } = default!;
    
    /// <summary>
    /// Gets or sets the email address of the user
    /// </summary>
    public string EmailAddress {get; set; } = default!;
    
    /// <summary>
    /// Gets or sets the password of the user's email address 
    /// </summary>
    public string PasswordHash { get; set; } = default!;
    
    /// <summary>
    /// Gets or sets role of the user
    /// </summary>
    public Role Role { get; set; } = default!;
}