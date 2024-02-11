namespace Freecer.Domain.Dtos.User;

public class SessionDto
{
    public SessionDto(UserDto user, DateTime expires)
    {
        User = user;
        Expires = expires;
    }
    
    public UserDto User { get; set; }
    public DateTime Expires { get; set; }
}