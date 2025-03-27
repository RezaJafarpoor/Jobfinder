using Jobfinder.Domain.Dtos;
using Jobfinder.Domain.Entities;

namespace Jobfinder.Domain.Interfaces;

public interface IJobSeekerProfileRepository
{
    Task<JobSeekerProfile> GetJobSeekerById(Guid jobSeekerId, CancellationToken cancellationToken);
    Task<bool> ApplyToJobOffer(Guid jobSeekerId, Guid jobOffer, CancellationToken cancellationToken);
    Task<List<JobSeekerProfile>> GetJobSeekers(CancellationToken cancellationToken);
    Task<bool> UpdateJobSeeker(Guid jobSeekerId);
    Task<List<JobApplication>> GetJobSeekerApplications(Guid jobSeekerId, CancellationToken cancellationToken);
}