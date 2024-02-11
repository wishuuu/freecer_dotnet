using Freecer.Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace Freecer.Infra;

public class UnitOfWork : IDisposable, IAsyncDisposable
{
    public readonly FreecerContext Context;

    public UnitOfWork(FreecerContext context)
    {
        Context = context;
    }

    public async Task<int> Commit()
    {
        foreach (var entity in Context.ChangeTracker
                     .Entries()
                     .Where(x => x.Entity is BaseEntity && x.State == EntityState.Added)
                     .Select(x => x.Entity)
                     .Cast<BaseEntity>())
        {
            entity.CreatedAt = DateTime.Now;
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