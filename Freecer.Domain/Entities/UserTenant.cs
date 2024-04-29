namespace Freecer.Domain.Entities;

public class UserTenant
{
    public int UserId { get; set; }
    public User User { get; set; } = null!;
    public int TenantId { get; set; }
    public Tenant Tenant { get; set; } = null!;
    public bool IsDefault { get; set; }
}