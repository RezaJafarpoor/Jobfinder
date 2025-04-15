using Jobfinder.Application.Commons;
using Jobfinder.Application.Dtos.Cvs;
using Jobfinder.Application.Interfaces.Common;

namespace Jobfinder.Application.Interfaces.UnitOfWorks;

public interface IJobSeekerCvUnitOfWork : IScopedService, IDisposable
{
    Task<Response<string>> CreateCvAndUpdateUsername(CreateCvDto cvDto, Guid userId,
        CancellationToken cancellationToken);

    Task BeginTransaction();
    Task CommitAsync();
    Task RollbackAsync();
    Task SaveChangesAsync();
}