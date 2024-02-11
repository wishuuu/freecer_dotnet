using Freecer.Domain.Entities;
using Freecer.Domain.Entities.Interfaces;
using Freecer.Domain.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace Freecer.Infra;

public class UnitOfWork : IDisposable, IAsyncDisposable
{
    public readonly FreecerContext Context;
    private readonly int? CurrentTenantId;

    public UnitOfWork(FreecerContext context, ICurrentTenant currentTenant)
    {
        Context = context;
        CurrentTenantId = currentTenant.TenantId;
    }

    public async Task<int> Commit()
    {
        foreach (var entity in Context.ChangeTracker
                     .Entries()
                     .Where(x => x is { Entity: BaseEntity, State: EntityState.Added })
                     .Select(x => x.Entity)
                     .Cast<BaseEntity>())
        {
            entity.CreatedAt = DateTime.Now;
        }

        foreach (var entity in Context.ChangeTracker
                     .Entries()
                     .Where(x => x is { Entity: ITenantSpecific, State: EntityState.Added })
                     .Select(x => x.Entity)
                     .Cast<ITenantSpecific>())
        {
            entity.TenantId = CurrentTenantId ?? throw new ArgumentNullException("Cannot create tenant specific entity without a tenant context");
        }

        return await Context.SaveChangesAsync();
    }

    public async Task Rollback()
    {
        await Context.DisposeAsync();
    }

    public async Task Migrate()
    {
        await Context.Database.MigrateAsync();
    }

    public void Dispose()
    {
        Context.Dispose();
    }

    public async ValueTask DisposeAsync()
    {
        await Context.DisposeAsync();
    }
}