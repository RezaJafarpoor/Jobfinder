using Jobfinder.Application.Interfaces.Repositories;
using Jobfinder.Domain.Entities;
using Jobfinder.Infrastructure.Persistence;
using Jobfinder.Infrastructure.Persistence.SqlServer;
using Microsoft.EntityFrameworkCore;

namespace Jobfinder.Infrastructure.Repositories;

internal class EmployerProfileRepository
    (ApplicationDbContext dbContext) : IEmployerProfileRepository
{
    public Task CreateProfile(EmployerProfile profile)
    {
         dbContext.Add(profile);
         return Task.CompletedTask;
    }

    public async Task<List<EmployerProfile>> GetProfiles(CancellationToken cancellationToken)
    {
        var profiles = await dbContext.EmployerProfiles
            .Include(c => c.Company)
            .ToListAsync(cancellationToken);
        return profiles;
    }

    public async Task<EmployerProfile?> GetProfileById(Guid userId, CancellationToken cancellationToken)
    {      
        var profile = await dbContext.EmployerProfiles
            .Include(u => u.Company)
            .FirstOrDefaultAsync(ep => ep.UserId == userId, cancellationToken);
        return profile;
    }

    public async Task<EmployerProfile?> GetProfileByUserId(Guid userId, CancellationToken cancellationToken)
    {
        var profile = await dbContext.EmployerProfiles
            .FirstOrDefaultAsync(ep => ep.UserId == userId, cancellationToken);
        return profile;
    }

    public async Task<bool> SaveChangesAsync(CancellationToken cancellationToken)
        => await dbContext.SaveChangesAsync(cancellationToken) > 0;
}