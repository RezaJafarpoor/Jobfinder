using Jobfinder.Application.Interfaces.Common;

namespace Jobfinder.Application.Interfaces.Identity;

public interface ITokenProvider : IScopedService
{
    public string GenerateJwtToken(Guid userId);
    public string GenerateJwtToken(Guid userId, string role);
    public string GenerateRefreshToken();
}