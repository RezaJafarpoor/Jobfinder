using Jobfinder.Application.Interfaces.Common;
using Jobfinder.Domain.Entities;

namespace Jobfinder.Application.Interfaces.Repositories;

public interface IRefreshTokenRepository: IScopedService
{
    Task AddToken( RefreshToken token);

    Task<RefreshToken?> FindTokenAsync(string token,CancellationToken cancellationToken);
    Task<bool> SaveChanges(CancellationToken cancellationToken);

}