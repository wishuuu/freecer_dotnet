using System.Globalization;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using Freecer.Domain;
using Freecer.Domain.Configs;
using Freecer.Domain.Entities;
using Freecer.Domain.Interfaces.Authorization;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;

namespace Freecer.Application.Authorization;

public class TokenService : ITokenService
{
    private readonly IOptions<AuthConfig> _authConfig;

    public TokenService(IOptions<AuthConfig> authConfig)
    {
        _authConfig = authConfig;
    }

    public string Create(User user, out Claim[] claims)
    {
        var issue = _authConfig.Value.Issuer;
        var audience = _authConfig.Value.Audience;
        
        claims = new[]
        {
            new Claim(FreecerClaims.UserId, user.Id.ToString()),
            new Claim(FreecerClaims.Username, user.Username),
            new Claim(FreecerClaims.Email, user.Email),
            new Claim(FreecerClaims.FirstName, user.FirstName),
            new Claim(FreecerClaims.LastName, user.LastName),
            // new Claim(FreecerClaims.TenantId, user.TenantId.ToString()),
            // new Claim(FreecerClaims.TenantName, user.Tenant.Name),
            new Claim(FreecerClaims.IsSuperUser, user.IsSuperUser.ToString()),
            new Claim(FreecerClaims.Expires, DateTime.Now.AddMinutes(_authConfig.Value.ExpiresInMinutes).ToString(CultureInfo.CurrentCulture))
        };

        var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_authConfig.Value.Secret));
        var creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);
        
        var token = new JwtSecurityToken(
            issuer: issue,
            audience: audience,
            claims: claims,
            expires: DateTime.Now.AddMinutes(_authConfig.Value.ExpiresInMinutes),
            signingCredentials: creds
        );

        var handler = new JwtSecurityTokenHandler();
        
        return handler.WriteToken(token);
    }

    public void Validate(string token, out Claim[] claims)
    {
        var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_authConfig.Value.Secret));
        var handler = new JwtSecurityTokenHandler();
        var validationParameters = new TokenValidationParameters
        {
            ValidateIssuer = true,
            ValidateAudience = true,
            ValidateLifetime = true,
            ValidateIssuerSigningKey = true,
            ValidIssuer = _authConfig.Value.Issuer,
            ValidAudience = _authConfig.Value.Audience,
            IssuerSigningKey = key
        };
        
        var principal = handler.ValidateToken(token, validationParameters, out var validatedToken);
        claims = principal.Claims.ToArray();
    }
}