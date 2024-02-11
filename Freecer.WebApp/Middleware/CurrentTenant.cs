using Freecer.Domain.Interfaces;
using Freecer.Infra;

namespace Freecer.WebApp.Middleware;

public class CurrentTenant : ICurrentTenant
{
    private readonly TenantContext _context;

    public CurrentTenant(TenantContext context)
    {
        _context = context;
    }

    public int? TenantId { get; set; }

    public async Task<bool> SetTenant(int tenantId)
    {
        var tenant = await _context.Tenants.FindAsync(tenantId);
        if (tenant is null || tenant.IsDeleted)
        {
            return false;
        }
        TenantId = tenantId;
        return true;
    }
}