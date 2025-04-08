using Jobfinder.Application.Interfaces.Common;
using Jobfinder.Domain.Entities;
using Jobfinder.Domain.Enums;

namespace Jobfinder.Application.Interfaces.Repositories;

public interface IJobApplicationRepository : IScopedService
{
    Task AddJobApplication(JobApplication application);
    Task<JobApplication?> Test();
    Task<List<JobApplication>> GetJobApplications(CancellationToken cancellationToken);
    Task<JobApplication?> GetJobApplication(Guid id, CancellationToken cancellationToken);
    Task<JobApplication?> GetJobApplication(Guid jobId,Guid applicationId, CancellationToken cancellationToken);

    
    Task<int> DeleteJobApplicationByJobSeekerId(Guid jobId, Guid applicationId, Guid jobSeekerId);
    Task<List<Cv?>?> GetCvsForJobOffer(Guid jobOfferId, CancellationToken cancellationToken);

    Task<bool> SaveChangesAsync(CancellationToken cancellationToken);
}