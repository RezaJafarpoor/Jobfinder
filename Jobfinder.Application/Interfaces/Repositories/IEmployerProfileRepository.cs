using Jobfinder.Domain.Entities;

namespace Jobfinder.Application.Interfaces.Repositories;

public interface IEmployerProfileRepository
{
    Task CreateProfile(User user);
    Task SaveChangesAsync(CancellationToken cancellationToken);


}