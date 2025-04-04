using Jobfinder.Application.Interfaces.Common;
using Jobfinder.Domain.Entities;

namespace Jobfinder.Application.Interfaces.Repositories;

public interface IJobSeekerProfileRepository : IScopedService
{
    Task CreateProfile(JobSeekerProfile jobSeekerProfile);
    Task<JobSeekerProfile?> GetProfileById(Guid userId, CancellationToken cancellationToken);
    Task Update(JobSeekerProfile jobSeekerProfile);
    Task<List<JobSeekerProfile>> GetJobSeekers(CancellationToken cancellationToken);
    Task<bool> SaveChangesAsync(CancellationToken cancellationToken);
}