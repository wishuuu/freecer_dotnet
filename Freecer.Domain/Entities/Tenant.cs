namespace Freecer.Domain.Entities;

public class Tenant : BaseEntity
{
    public string Name { get; set; } = null!;
    
    public virtual ICollection<UserTenant> UserTenants { get; set; } = new List<UserTenant>();
    public virtual ICollection<User> Users { get; set; } = new List<User>();
}