using Jobfinder.Application.Interfaces.Common;
using Jobfinder.Domain.Entities;

namespace Jobfinder.Application.Interfaces.Repositories;

public interface IRefreshTokenRepository: IScopedService
{
    Task<bool> AddTokenForUser( RefreshToken token);

    Task<RefreshToken?> FindAsync(string token,CancellationToken cancellationToken);
    Task<bool> SaveChanges(CancellationToken cancellationToken);

}