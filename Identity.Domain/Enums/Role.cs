namespace Identity.Domain.Enums;

/// <summary>
/// Enum representing different roles  that a clint  can have in the system 
/// </summary>
public enum Role
{
    /// <summary>
    /// Standard guest role
    /// </summary>
    Guest,
    
    /// <summary>
    /// Host role that a clint can have in the system
    /// </summary>
    Host,
    
    /// <summary>
    /// Administrator role with elevated privileges
    /// </summary>
    Admin
}