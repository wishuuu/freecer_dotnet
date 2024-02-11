using Freecer.Domain.Entities;
using Freecer.Domain.Interfaces.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

namespace Freecer.Application.Authorization;

public class AuthService : IAuthService
{
    private readonly DbSet<User> _users;
    private readonly IPasswordHasher<User> _passwordHasher;

    public AuthService(DbSet<User> users)
    {
        _users = users;
        _passwordHasher = new PasswordHasher<User>();
    }

    public bool TryAuthenticate(string username, string password, out User? user)
    {
        user = null;
        user = _users.FirstOrDefault(u => u.Username == username);
        if (user == null) return false;
        
        var result = _passwordHasher.VerifyHashedPassword(user, user.PasswordHash, password + user.Salt);
        if (result == PasswordVerificationResult.Success) return true;
        
        user = null;
        return false;
    }
}