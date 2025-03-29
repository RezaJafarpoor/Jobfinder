using Jobfinder.Domain.Entities;

namespace Jobfinder.Application.Interfaces.Identity;

public interface ITokenProvider
{
    public string GenerateJwtToken(User user);
    public string GenerateRefreshToken();
}