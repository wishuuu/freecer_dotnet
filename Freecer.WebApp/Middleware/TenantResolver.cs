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
        if (string.IsNullOrEmpty(tenant))
        {
            context.Response.StatusCode = StatusCodes.Status400BadRequest;
            await context.Response.WriteAsync("Tenant header is required");
            return;
        }

        if (!int.TryParse(tenant, out int tenantId))
        {
            context.Response.StatusCode = StatusCodes.Status400BadRequest;
            await context.Response.WriteAsync("Tenant header should contain integer value");
            return;
        }

        await currentTenant.SetTenant(tenantId);
        await _next(context);
    }
}