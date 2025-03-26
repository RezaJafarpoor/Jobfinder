using Jobfinder.Domain.Dtos;
using Jobfinder.Domain.Entities;

namespace Jobfinder.Domain.Interfaces;

public interface IJobSeekerRepository
{
    Task<JobSeekerProfile> GetJobSeekerById(Guid id);
    Task<List<JobSeekerProfile>> GetJobSeekers();
    Task<bool> UpdateJobSeeker();
}