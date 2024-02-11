using Freecer.Domain.Interfaces;
using Freecer.Infra;
using Freecer.WebApp.Middleware;

namespace Freecer.Application.Middleware;

public class TenantResolver
{
    private readonly RequestDelegate _next;

    public TenantResolver(RequestDelegate next)
    {
        _next = next;
    }

    public async Task Invoke(HttpContext context, ICurrentTenant currentTenant)
    {
        var tenant = context.Request.Headers["Tenant"];

        if (!int.TryParse(tenant, out int tenantId))
        {
            await _next(context);
            return;
        }

        await currentTenant.SetTenant(tenantId);
        await _next(context);
    }
}