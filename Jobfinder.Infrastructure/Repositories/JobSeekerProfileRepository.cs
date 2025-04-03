using Jobfinder.Application.Interfaces.Repositories;
using  Jobfinder.Domain.Entities;
using Jobfinder.Infrastructure.Persistence;
using Microsoft.EntityFrameworkCore; 

namespace Jobfinder.Infrastructure.Repositories;

internal class JobSeekerProfileRepository
    (ApplicationDbContext dbContext) : IJobSeekerProfileRepository
{
    public Task CreateProfile(JobSeekerProfile jobSeekerProfile)
    {
        dbContext.JobSeekerProfiles.Add(jobSeekerProfile);
        return Task.CompletedTask;
    }

    public async Task<JobSeekerProfile?> GetProfileById(Guid userId, CancellationToken cancellationToken)
    {
        var profile = await dbContext.JobSeekerProfiles.Include(u => u.User)
            .FirstOrDefaultAsync(jsp => jsp.Id == userId, cancellationToken);
        return profile;
    }

    public Task Update(JobSeekerProfile jobSeekerProfile)
    {
        dbContext.Update(jobSeekerProfile);
        return Task.CompletedTask;
    }

    public async Task<List<JobSeekerProfile>> GetJobSeekers(CancellationToken cancellationToken)
    {
        var jobSeekers = await dbContext.JobSeekerProfiles.AsNoTracking().ToListAsync(cancellationToken);
        return jobSeekers;
    }

    public async Task<bool> SaveChangesAsync(CancellationToken cancellationToken)
        => await dbContext.SaveChangesAsync(cancellationToken) > 0;
}