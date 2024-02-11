using Freecer.Domain.Entities;

namespace Freecer.Domain.Interfaces.Authorization;

public interface IAuthService
{
    bool TryAuthenticate(string username, string password, out User? user);
}