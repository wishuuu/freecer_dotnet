using Freecer.Domain.Entities;

namespace Freecer.Domain.Interfaces.Authorization;

public interface ICurrentTenant
{
    public int? TenantId { get; }
    public Tenant? Tenant { get; }
    public bool IsSet => TenantId.HasValue;
    
    Task<bool> SetTenant(int tenantId);
}
