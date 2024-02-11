namespace Freecer.Domain.Entities.Interfaces;

public interface ITenantSpecific
{
    public int TenantId { get; set; }
    public Tenant Tenant { get; set; }
}