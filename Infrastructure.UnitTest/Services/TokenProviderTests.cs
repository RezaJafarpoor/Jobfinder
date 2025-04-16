using Jobfinder.Application.Interfaces.Identity;
using Jobfinder.Domain.Enums;
using Jobfinder.Infrastructure.Identity;
using Microsoft.Extensions.Options;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;

namespace Infrastructure.UnitTest.Services;

public class TokenProviderTests
{
    //TODO:Check For validation of option pattern in DI
    

    [Fact]
    public void GenerateJwtToken_ShouldReturnString()
    {

        // ARRANGE
        var jwtOption = Options.Create(new JwtSetting
        {
            Secret = "iaJQj7RA97U0V0nJujdtYg2dV5kheGoSqSx8zuceD1w=",
            Issuer = "https://localhost:7093",
            ExpirationTimeInMinute = 30,
            Audience = "https://localhost:7093"
        });
        var tokenProvider = new TokenProvider(jwtOption);
        var userId = Guid.NewGuid();
        // ACT
        var token = tokenProvider.GenerateJwtToken(userId);
        
        // ASSERT
        var handler = new JwtSecurityTokenHandler();
        var jwt = handler.ReadJwtToken(token);
        Assert.Contains(jwt.Claims, c => c.Type == JwtRegisteredClaimNames.Sub && c.Value == userId.ToString());
    }
    [Fact]
    public void GenerateJwtTokenWithRole_ShouldReturnString()
    {

        // ARRANGE
        var jwtOption = Options.Create(new JwtSetting
        {
            Secret = "iaJQj7RA97U0V0nJujdtYg2dV5kheGoSqSx8zuceD1w=",
            Issuer = "https://localhost:7093",
            ExpirationTimeInMinute = 30,
            Audience = "https://localhost:7093"
        });
        var tokenProvider = new TokenProvider(jwtOption);
        var userId = Guid.NewGuid();
        // ACT
        var token = tokenProvider.GenerateJwtToken(userId, Roles.Employer.ToString());
        
        // ASSERT
        var handler = new JwtSecurityTokenHandler();
        var jwt = handler.ReadJwtToken(token);
        Assert.Contains(jwt.Claims, c => c.Type == JwtRegisteredClaimNames.Sub && c.Value == userId.ToString());
        Assert.Contains(jwt.Claims, c => c.Type == ClaimTypes.Role && c.Value == Roles.Employer.ToString());
        
    }
    

}