using Freecer.Domain.Entities;
using Freecer.Domain.Interfaces.Authorization;
using Freecer.Infra;
using Microsoft.EntityFrameworkCore;

namespace Freecer.WebApp.Middleware;

public class CurrentUser : ICurrentUser
{
    private readonly UserContext _context;

    public CurrentUser(UserContext context)
    {
        _context = context;
    }

    public int? UserId { get; set; }
    public User? User => _context.Users.AsNoTracking().SingleOrDefault(u => u.Id == UserId);

    public async Task<bool> SetUser(int userId)
    {
        var user = await _context.Users.AsNoTracking().SingleOrDefaultAsync(u => u.Id == userId);
        if (user is null || user.IsDeleted)
        {
            return false;
        }
        UserId = userId;
        return true;
    }
}