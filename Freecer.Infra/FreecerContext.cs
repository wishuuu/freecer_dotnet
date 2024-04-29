using Freecer.Domain.Entities;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

namespace Freecer.Infra;

public sealed class FreecerContext : DbContext
{
    public FreecerContext(DbContextOptions<FreecerContext> options) : base(options)
    {
    }

    public DbSet<User> Users => Set<User>();
    public DbSet<Tenant> Tenants => Set<Tenant>();
    public DbSet<UserTenant> UserTenants => Set<UserTenant>();

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);

        #region User

        modelBuilder.Entity<User>()
            .HasKey(u => u.Id);

        modelBuilder.Entity<User>()
            .Property(u => u.Id)
            .ValueGeneratedOnAdd();

        modelBuilder.Entity<User>()
            .Property(u => u.Username)
            .IsRequired();

        modelBuilder.Entity<User>()
            .Property(u => u.PasswordHash)
            .IsRequired();

        modelBuilder.Entity<User>()
            .Property(u => u.Salt)
            .IsRequired();

        modelBuilder.Entity<User>()
            .Property(u => u.IsSuperUser)
            .IsRequired();

        modelBuilder.Entity<User>()
            .Property(u => u.Email)
            .IsRequired();

        modelBuilder.Entity<User>()
            .Property(u => u.FirstName)
            .IsRequired();

        modelBuilder.Entity<User>()
            .Property(u => u.LastName)
            .IsRequired();

        #endregion

        #region Tenant

        modelBuilder.Entity<Tenant>()
            .HasKey(t => t.Id);

        modelBuilder.Entity<Tenant>()
            .Property(t => t.Id)
            .ValueGeneratedOnAdd();

        modelBuilder.Entity<Tenant>()
            .Property(t => t.Name)
            .IsRequired();

        #endregion

        #region UserTenant

        modelBuilder.Entity<User>()
            .HasMany(u => u.Tenants)
            .WithMany(t => t.Users)
            .UsingEntity<UserTenant>(
                l => l.HasOne(ut => ut.Tenant).WithMany(t => t.UserTenants).HasForeignKey(ut => ut.TenantId),
                r => r.HasOne(ut => ut.User).WithMany(u => u.UserTenants).HasForeignKey(ut => ut.UserId),
                j =>
                {
                    j.HasKey(ut => new { ut.UserId, ut.TenantId });
                    j.Property(ut => ut.IsDefault).IsRequired();
                });

        #endregion

        #region Default data

        IPasswordHasher<User> passwordHasher = new PasswordHasher<User>();

        modelBuilder.Entity<User>().HasData(new User
        {
            Id = 1,
            Username = "admin",
            PasswordHash = passwordHasher.HashPassword(null, "admin" + "64312589"),
            Salt = "64312589",
            IsSuperUser = true,
            Email = "kontakt.wiszowaty.o@gmail.com",
            FirstName = "Oskar",
            LastName = "Wiszowaty",
        });

        modelBuilder.Entity<Tenant>().HasData(new Tenant
        {
            Id = 1,
            Name = "OvSoft"
        });

        #endregion
    }
}