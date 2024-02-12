using Freecer.Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace Freecer.Infra;

public class TenantContext : DbContext
{
    public TenantContext(DbContextOptions<TenantContext> options) : base(options)
    {
    }
    
    public DbSet<Tenant> Tenants => Set<Tenant>();
}