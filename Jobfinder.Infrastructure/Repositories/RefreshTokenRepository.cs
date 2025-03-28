using Jobfinder.Domain.Entities;
using Jobfinder.Domain.Interfaces;
using Jobfinder.Infrastructure.Persistence;

namespace Jobfinder.Infrastructure.Repositories;

internal class RefreshTokenRepository
    (ApplicationDbContext dbContext) : IRefreshTokenRepository
{
    public async Task<bool> AddTokenForUser(RefreshToken token)
    {
        dbContext.Add(token);
        return await dbContext.SaveChangesAsync() > 0;
    }
}