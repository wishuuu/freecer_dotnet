using Freecer.Domain.Entities;

namespace Freecer.Domain.Interfaces.Authorization;

public interface ICurrentUser
{
    public int? UserId { get; }
    public User? User { get; }
    public bool IsSet => UserId.HasValue;
    
    public Task<bool> SetUser(int userId);
}