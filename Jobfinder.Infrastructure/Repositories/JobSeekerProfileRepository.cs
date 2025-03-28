using  Jobfinder.Domain.Entities;
using Jobfinder.Domain.Interfaces;
using Jobfinder.Infrastructure.Persistence;
using Microsoft.EntityFrameworkCore; 

namespace Jobfinder.Infrastructure.Repositories;

internal class JobSeekerProfileRepository
    (ApplicationDbContext dbContext) : IJobSeekerProfileRepository
{
    public Task CreateProfile(User user)
    {
        var profile = new JobSeekerProfile(user, null, null);
        dbContext.JobSeekerProfiles.Add(profile);
        return Task.CompletedTask;
    }

    public async Task<User?> GetUserById(Guid jobSeekerId, CancellationToken cancellationToken)
    {
        return await dbContext.JobSeekerProfiles.AsNoTracking()
            .Where(jsp => jsp.Id == jobSeekerId)
            .Select(jsp => jsp.User).FirstOrDefaultAsync(cancellationToken);
    }

    public Task Update(User user)
    {
        dbContext.Update(user);
        return Task.CompletedTask;
    }

    public async Task<bool> SaveChangesAsync(CancellationToken cancellationToken)
        => await dbContext.SaveChangesAsync(cancellationToken) > 0;
}