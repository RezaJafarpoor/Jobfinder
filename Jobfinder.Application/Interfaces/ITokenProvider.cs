using Jobfinder.Domain.Entities;

namespace Jobfinder.Application.Interfaces;

public interface ITokenProvider
{
    public string GenerateJwtToken(User user);
    public string GenerateRefreshToken();
}