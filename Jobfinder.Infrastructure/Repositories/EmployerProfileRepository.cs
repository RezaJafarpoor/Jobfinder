using Jobfinder.Domain.Entities;
using Jobfinder.Domain.Interfaces;
using Jobfinder.Infrastructure.Persistence;

namespace Jobfinder.Infrastructure.Repositories;

internal class EmployerProfileRepository
    (ApplicationDbContext dbContext) : IEmployerProfileRepository
{
    public Task CreateProfile(User user)
    {
        var profile = new EmployerProfile(user);
         dbContext.Add(profile);
         return Task.CompletedTask;
    }
}