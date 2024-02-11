using Freecer.Infra;

namespace Freecer.WebApp.Middleware;

public interface ICurrentTenant
{
    public int TenantId { get; }
    public Domain.Entities.Tenant? Tenant { get; }
    
    Task<bool> SetTenant(int tenantId);
}

public class CurrentTenant : ICurrentTenant
{
    private readonly UnitOfWork _unitOfWork;
    public CurrentTenant(UnitOfWork unitOfWork)
    {
        _unitOfWork = unitOfWork;
    }
    
    public int TenantId { get; set; }
    public Domain.Entities.Tenant? Tenant { get; set; }

    public async Task<bool> SetTenant(int tenantId)
    {
        var tenant = await _unitOfWork.Context.Tenants.FindAsync(tenantId);
        if (tenant is null || tenant.IsDeleted)
        {
            throw new Exception("Tenant not found");
        }
        TenantId = tenantId;
        Tenant = tenant;
        return true;
    }
}