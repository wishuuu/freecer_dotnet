﻿namespace Freecer.Domain.Dtos.Register;

public class RegisterDto
{
    public required string Username { get; set; }
    public required string Email { get; set; }
    public required string Password { get; set; }
    public required string ConfirmPassword { get; set; }
    public required string FirstName { get; set; }
    public required string LastName { get; set; }
    public string? PhoneNumber { get; set; }
}