using Jobfinder.Domain.Entities;

namespace Jobfinder.Application.Interfaces.Repositories;

public interface IEmployerProfileRepository
{
    Task CreateProfile(EmployerProfile profile);
    Task<List<EmployerProfile>> GetProfiles(CancellationToken cancellationToken);
    Task<bool> SaveChangesAsync(CancellationToken cancellationToken);
    Task<EmployerProfile?> GetProfileById(Guid profileId, CancellationToken cancellationToken);
    Task<EmployerProfile?> GetProfileByUserId(Guid userId, CancellationToken cancellationToken);


}