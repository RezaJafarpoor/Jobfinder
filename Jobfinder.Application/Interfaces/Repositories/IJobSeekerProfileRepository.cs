using Jobfinder.Domain.Entities;

namespace Jobfinder.Application.Interfaces.Repositories;

public interface IJobSeekerProfileRepository
{
    Task CreateProfile(JobSeekerProfile jobSeekerProfile);
    Task<JobSeekerProfile?> GetUserById(Guid jobSeekerId, CancellationToken cancellationToken);
    Task Update(JobSeekerProfile jobSeekerProfile);
    Task<List<JobSeekerProfile>> GetJobSeekers(CancellationToken cancellationToken);
    Task<bool> SaveChangesAsync(CancellationToken cancellationToken);
}