using Jobfinder.Application.Interfaces.Repositories;
using Jobfinder.Domain.Entities;
using Jobfinder.Infrastructure.Persistence;
using Microsoft.EntityFrameworkCore;

namespace Jobfinder.Infrastructure.Repositories;

internal class RefreshTokenRepository
    (ApplicationDbContext dbContext) : IRefreshTokenRepository
{
    public async Task<bool> AddTokenForUser(RefreshToken token)
    {
        dbContext.Add(token);
        return await dbContext.SaveChangesAsync() > 0;
    }

    public async Task<RefreshToken?> FindAsync(string token,CancellationToken cancellationToken)
    {
        var result = await dbContext.RefreshTokens.Include(rt => rt.User)
            .FirstOrDefaultAsync(rt => rt.Token == token, cancellationToken);
        return result;
    }

    public async Task<bool> SaveChanges(CancellationToken cancellationToken)
        => await dbContext.SaveChangesAsync(cancellationToken) > 0;
}