namespace Freecer.Domain.Interfaces;

public interface ICurrentTenant
{
    public int? TenantId { get; }
    public bool IsSet => TenantId.HasValue;
    
    Task<bool> SetTenant(int tenantId);
}
