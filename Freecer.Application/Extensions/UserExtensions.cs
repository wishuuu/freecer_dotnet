using System.Security.Cryptography;
using Freecer.Domain.Entities;
using Microsoft.AspNetCore.Identity;

namespace Freecer.Application.Extensions;

public static class UserExtensions
{
    public static void SetPassword(this User user, string password)
    {
        var passwordHasher = new PasswordHasher<User>();
        var random = new Random(DateTime.Now.Microsecond * DateTime.Now.Second);
        var salt = new byte[16];
        random.NextBytes(salt);
        user.Salt = Convert.ToBase64String(salt);
        user.PasswordHash = passwordHasher.HashPassword(user, password + user.Salt);
    }
}