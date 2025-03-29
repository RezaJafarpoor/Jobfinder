using Jobfinder.Domain.Entities;

namespace Jobfinder.Application.Interfaces.Repositories;

public interface IEmployerProfileRepository
{
    Task CreateProfile(EmployerProfile profile);
    Task SaveChangesAsync(CancellationToken cancellationToken);


}