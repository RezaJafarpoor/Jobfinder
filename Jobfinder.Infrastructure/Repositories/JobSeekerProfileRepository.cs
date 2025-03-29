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

    public async Task<JobSeekerProfile?> GetUserById(Guid jobSeekerId, CancellationToken cancellationToken)
    {
        return await dbContext.JobSeekerProfiles
            .FirstOrDefaultAsync(jsp => jsp.Id == jobSeekerId, cancellationToken);
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