using Jobfinder.Domain.Dtos;
using Jobfinder.Domain.Entities;

namespace Jobfinder.Domain.Interfaces;

public interface IJobSeekerProfileRepository
{
    Task CreateProfile(User user);
    Task<User?> GetUserById(Guid jobSeekerId, CancellationToken cancellationToken);
    Task Update(User user);
    Task<bool> SaveChangesAsync(CancellationToken cancellationToken);
}