using Freecer.Domain;
using Freecer.Domain.Interfaces.Authorization;
using Freecer.Infra;
using Microsoft.EntityFrameworkCore;

namespace Freecer.WebApp.Middleware;

public class UserResolver
{
    private readonly RequestDelegate _next;

    public UserResolver(RequestDelegate next)
    {
        _next = next;
    }

    public async Task Invoke(HttpContext context, UserContext userContext, ICurrentUser currentUser, ITokenService tokenService)
    {
        var claims = context.User.Claims.ToList();
        if (!int.TryParse(claims.SingleOrDefault(c => c.Type == FreecerClaims.UserId)?.Value, out var userId))
        {
            await _next(context);
            return;
        }
        var user = await userContext.Users.AsNoTracking().FirstOrDefaultAsync(u => u.Id == userId);
        if (user is null || user.IsDeleted)
        {
            await _next(context);
            return;
        }

        await currentUser.SetUser(userId);
        await _next(context);
    }
}