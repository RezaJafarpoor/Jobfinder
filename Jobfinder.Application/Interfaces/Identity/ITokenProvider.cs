using Jobfinder.Application.Interfaces.Common;
using Jobfinder.Domain.Commons.Identity;

namespace Jobfinder.Application.Interfaces.Identity;

public interface ITokenProvider : IScopedService
{
    public string GenerateJwtToken(Guid userId, string role);
    public string GenerateRefreshToken();
}