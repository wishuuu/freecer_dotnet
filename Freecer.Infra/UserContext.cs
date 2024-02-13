using Freecer.Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace Freecer.Infra;

public class UserContext : DbContext
{
    public UserContext(DbContextOptions<UserContext> options) : base(options)
    {
    }
    
    public DbSet<User> Users => Set<User>();
}