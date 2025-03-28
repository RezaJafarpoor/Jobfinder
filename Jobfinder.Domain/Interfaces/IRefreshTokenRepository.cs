using Jobfinder.Domain.Entities;

namespace Jobfinder.Domain.Interfaces;

public interface IRefreshTokenRepository
{
    Task<bool> AddTokenForUser( RefreshToken token);

}