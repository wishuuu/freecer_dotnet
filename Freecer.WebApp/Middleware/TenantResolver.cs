using Freecer.Domain;
using Freecer.Domain.Interfaces;
using Freecer.Domain.Interfaces.Authorization;
using Freecer.Infra;
using Freecer.WebApp.Middleware;
using Microsoft.EntityFrameworkCore;

namespace Freecer.Application.Middleware;

public class TenantResolver
{
    private readonly RequestDelegate _next;

    public TenantResolver(RequestDelegate next)
    {
        _next = next;
    }

    public async Task Invoke(HttpContext context, TenantContext tenantContext, ICurrentTenant currentTenant)
    {
        var claims = context.User.Claims.ToList();
        if (!int.TryParse(claims.SingleOrDefault(c => c.Type == FreecerClaims.TenantId)?.Value, out var tenantId))
        {
            await _next(context);
            return;
        }
        var user = await tenantContext.Tenants.AsNoTracking().FirstOrDefaultAsync(u => u.Id == tenantId);
        if (user is null || user.IsDeleted)
        {
            await _next(context);
            return;
        }

        await currentTenant.SetTenant(tenantId);
        await _next(context);
    }
}