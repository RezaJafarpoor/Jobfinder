using Jobfinder.Domain.Entities;

namespace Jobfinder.Application.Interfaces.Repositories;

public interface IRefreshTokenRepository
{
    Task<bool> AddTokenForUser( RefreshToken token);

}