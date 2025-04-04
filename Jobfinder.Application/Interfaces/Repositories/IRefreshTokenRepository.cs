using Jobfinder.Application.Interfaces.Common;
using Jobfinder.Domain.Entities;

namespace Jobfinder.Application.Interfaces.Repositories;

public interface IRefreshTokenRepository: IScopedService
{
    Task<bool> AddTokenForUser( RefreshToken token);

}