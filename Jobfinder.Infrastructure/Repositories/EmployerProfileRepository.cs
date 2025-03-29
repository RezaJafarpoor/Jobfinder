using Jobfinder.Application.Interfaces.Repositories;
using Jobfinder.Domain.Entities;
using Jobfinder.Infrastructure.Persistence;

namespace Jobfinder.Infrastructure.Repositories;

internal class EmployerProfileRepository
    (ApplicationDbContext dbContext) : IEmployerProfileRepository
{
    public Task CreateProfile(EmployerProfile profile)
    {
         dbContext.Add(profile);
         return Task.CompletedTask;
    }

    public async Task SaveChangesAsync(CancellationToken cancellationToken)
        => await dbContext.SaveChangesAsync(cancellationToken);
}