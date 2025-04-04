using Jobfinder.Application.Interfaces.Common;
using Jobfinder.Domain.Entities;

namespace Jobfinder.Application.Interfaces.Repositories;

public interface IJobApplicationRepository : IScopedService
{
    Task AddJobApplication(JobApplication application);
    Task<List<JobApplication>> GetJobApplications(CancellationToken cancellationToken);
    Task<JobApplication?> GetJobApplication(Guid id, CancellationToken cancellationToken);
    Task<int> DeleteJobApplicationByJobSeekerId(Guid jobSeekerId);
    Task<List<Cv?>?> GetCvsForJobOffer(Guid jobOfferId, CancellationToken cancellationToken);

    Task<bool> SaveChangesAsync(CancellationToken cancellationToken);
}