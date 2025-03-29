using Jobfinder.Application.Interfaces.Identity;
using Jobfinder.Domain.Entities;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Security.Cryptography;
using System.Text;

namespace Jobfinder.Infrastructure.Identity;

internal sealed class TokenProvider(IOptions<JwtSetting> jwtSetting) : ITokenProvider
{
    public string GenerateJwtToken(User user)
    {
        var secretKey = jwtSetting.Value.Secret;
        var claims = new List<Claim>
        {
            new Claim(JwtRegisteredClaimNames.Sub, user.Id.ToString()),
            new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString()),
            new Claim(JwtRegisteredClaimNames.Aud, jwtSetting.Value.Audience)
            
        };

        var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(jwtSetting.Value.Secret));
        var credential = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);
        var token = new JwtSecurityToken(
            issuer: jwtSetting.Value.Issuer,
            claims: claims,
            expires: DateTime.UtcNow.AddMinutes(jwtSetting.Value.ExpirationTimeInMinute),
            signingCredentials: credential
        );
        return new JwtSecurityTokenHandler().WriteToken(token);
    }
    
    public string GenerateRefreshToken()
        => Convert.ToBase64String(RandomNumberGenerator.GetBytes(32));
}