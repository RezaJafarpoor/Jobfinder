using Jobfinder.Domain.Entities;
using Jobfinder.Domain.Interfaces;
using Jobfinder.Infrastructure.Persistence;

namespace Jobfinder.Infrastructure.Repositories;

internal class JobSeekerRepository(ApplicationDbContext dbContext) : IJobSeekerRepository
{
    public Task<JobSeekerProfile> GetJobSeekerById(Guid id)
    {
        throw new NotImplementedException();
    }

    public Task<List<JobSeekerProfile>> GetJobSeekers()
    {
        throw new NotImplementedException();
    }

    public Task<bool> UpdateJobSeeker()
    {
        throw new NotImplementedException();
    }
}