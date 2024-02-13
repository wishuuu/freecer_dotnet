using System.Security.Claims;
using Freecer.Domain;

namespace Freecer.Application.Authorization;

public static class UserExtensions
{
    public static int GetId(this ClaimsPrincipal user)
    {
        return int.Parse(user.FindFirst(FreecerClaims.UserId)?.Value ??"0");
    }
    
    public static DateTime GetExpires(this ClaimsPrincipal user)
    {
        return DateTime.Parse(user.FindFirst(FreecerClaims.Expires)?.Value ?? DateTime.MinValue.ToString());
    }
}