namespace Freecer.Domain.Dtos.User;

public class UserDto
{
    public required string Username { get; set; }
    public required string Email { get; set; }
    public required string FirstName { get; set; }
    public required string LastName { get; set; }
    public string? PhoneNumber { get; set; }
    public string? ProfilePicture { get; set; }
}