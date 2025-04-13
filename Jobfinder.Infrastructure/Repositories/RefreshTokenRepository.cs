using Jobfinder.Application.Interfaces.Repositories;
using Jobfinder.Domain.Entities;
using Jobfinder.Infrastructure.Persistence;
using Jobfinder.Infrastructure.Persistence.SqlServer;
using Microsoft.EntityFrameworkCore;

namespace Jobfinder.Infrastructure.Repositories;

internal class RefreshTokenRepository
    (ApplicationDbContext dbContext) : IRefreshTokenRepository
{
    public Task AddToken(RefreshToken token)
    {
        dbContext.Add(token);
        return Task.CompletedTask;
    }

    public async Task<RefreshToken?> FindTokenAsync(string token,CancellationToken cancellationToken)
    {
        var result = await dbContext.RefreshTokens.Include(rt => rt.User)
            .FirstOrDefaultAsync(rt => rt.Token == token, cancellationToken);
        return result;
    }

    public async Task<bool> SaveChanges(CancellationToken cancellationToken)
    {
        var result = await dbContext.SaveChangesAsync(cancellationToken) ;
        return result > 0;
    }
}