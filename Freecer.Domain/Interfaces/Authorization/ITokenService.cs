using System.Security.Claims;
using Freecer.Domain.Entities;

namespace Freecer.Domain.Interfaces.Authorization;

public interface ITokenService
{
    string Create(User user, out Claim[] claims);
    void Validate(string token, out Claim[] claims);
}