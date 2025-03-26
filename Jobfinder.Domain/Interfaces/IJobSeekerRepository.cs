using Jobfinder.Domain.Dtos;
using Jobfinder.Domain.Entities;

namespace Jobfinder.Domain.Interfaces;

public interface IJobSeekerRepository
{
    Task<bool> CreateJobSeekerProfile(User user, CancellationToken cancellationToken );
}