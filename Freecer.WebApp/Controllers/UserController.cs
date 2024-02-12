using AutoMapper;
using Freecer.Application.Authorization;
using Freecer.Application.Extensions;
using Freecer.Domain.Configs;
using Freecer.Domain.Dtos.Login;
using Freecer.Domain.Dtos.Register;
using Freecer.Domain.Dtos.User;
using Freecer.Domain.Entities;
using Freecer.Domain.Interfaces.Authorization;
using Freecer.Infra;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;

namespace Freecer.WebApp.Controllers;

public class UserController : ApiController
{
    private readonly UnitOfWork _unitOfWork;
    private readonly IAuthService _authService;
    private readonly ITokenService _tokenService;
    private readonly IOptions<AuthConfig> _authConfig;
    private readonly IMapper _mapper;

    public UserController(UnitOfWork unitOfWork, IAuthService authService, ITokenService tokenService, IOptions<AuthConfig> authConfig, IMapper mapper)
    {
        _unitOfWork = unitOfWork;
        _authService = authService;
        _tokenService = tokenService;
        _authConfig = authConfig;
        _mapper = mapper;
    }

    [HttpGet]
    [ProducesResponseType(typeof(IEnumerable<User>), StatusCodes.Status200OK)]
    public async Task<IActionResult> Get()
    {
        var users = await _unitOfWork.Context.Users.ToListAsync();
        return Ok(users);
    }
    
    [HttpPost("login")]
    [ProducesResponseType(typeof(string), StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status401Unauthorized)]
    public async Task<IActionResult> Login([FromBody] LoginDto model)
    {
        if (!_authService.TryAuthenticate(model.Username, model.Password, out var user))
            return Unauthorized();
        
        var token = _tokenService.Create(user, out var claims);
        Response.Cookies.Append("freecer_jwt", token, new CookieOptions
        {
            HttpOnly = true,
            SameSite = SameSiteMode.None,
            Secure = true,
            Expires = DateTime.Now + TimeSpan.FromMinutes(_authConfig.Value.ExpiresInMinutes)
        });
        return Ok(token);
    }
    
    [HttpPost("logout")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    public IActionResult Logout()
    {
        Response.Cookies.Delete("freecer_jwt");
        return Ok();
    }
    
    [HttpGet("me")]
    [ProducesResponseType(typeof(SessionDto), StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status401Unauthorized)]
    public async Task<IActionResult> Me()
    {
        var user = await _unitOfWork.Context.Users.FirstOrDefaultAsync(u => u.Id == User.GetId());
        if (user == null)
            return Unauthorized();

        var userDto = _mapper.Map<UserDto>(user);
        var session = new SessionDto(userDto, User.GetExpires());
        
        return Ok(session);
    }
    
    [HttpPost("checkusername")]
    [ProducesResponseType(typeof(bool), StatusCodes.Status200OK)]
    public async Task<IActionResult> CheckUsername([FromBody] RegisterUsernameCheckDto model)
    {
        var exists = await _unitOfWork.Context.Users.AnyAsync(u => u.Username == model.Username || u.Email == model.Email);
        return Ok(exists);
    }
    
    [HttpPost("register")]
    [ProducesResponseType(typeof(string), StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status409Conflict)]
    public async Task<IActionResult> Register([FromBody] RegisterDto model)
    {
        if (await _unitOfWork.Context.Users.AnyAsync(u => u.Username == model.Username))
            return Conflict("Username already exists");
        
        if (await _unitOfWork.Context.Users.AnyAsync(u => u.Email == model.Email))
            return Conflict("Email already exists");

        if (model.Password != model.ConfirmPassword)
            return BadRequest("Passwords do not match");
        
        var user = new User
        {
            Username = model.Username,
            Email = model.Email,
            FirstName = model.FirstName,
            LastName = model.LastName,
            PhoneNumber = model.PhoneNumber,
            PasswordHash = string.Empty,
            Salt = string.Empty,
        };
        user.SetPassword(model.Password);
        await _unitOfWork.Context.Users.AddAsync(user);
        await _unitOfWork.Context.SaveChangesAsync();
        
        var token = _tokenService.Create(user, out var claims);
        Response.Cookies.Append("freecer_jwt", token, new CookieOptions
        {
            HttpOnly = true,
            SameSite = SameSiteMode.None,
            Secure = true,
            Expires = DateTime.Now + TimeSpan.FromMinutes(_authConfig.Value.ExpiresInMinutes)
        });
        
        return Ok("User registered successfully");
    }
}