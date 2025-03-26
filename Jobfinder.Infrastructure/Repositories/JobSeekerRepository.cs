using Jobfinder.Domain.Entities;
using Jobfinder.Domain.Interfaces;
using Jobfinder.Infrastructure.Persistence;

namespace Jobfinder.Infrastructure.Repositories;

internal class JobSeekerRepository(ApplicationDbContext dbContext) : IJobSeekerRepository
{
    public async Task<bool> CreateJobSeekerProfile(User user, CancellationToken cancellationToken)
    {
        var profile = new JobSeekerProfile { User = user};
        dbContext.JobSeekerProfiles.Add(profile);
        return await dbContext.SaveChangesAsync(cancellationToken) > 0; 
    }
}