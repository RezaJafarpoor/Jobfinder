using Jobfinder.Application.Commons;
using Jobfinder.Application.Dtos.Cvs;
using Jobfinder.Application.Interfaces.Common;

namespace Jobfinder.Application.Interfaces.UnitOfWorks;

public interface IJobSeekerCvUnitOfWork : IScopedService
{
    Task<Response<string>> CreateCvAndUpdateUsername(CreateCvDto cvDto, Guid userId,
        CancellationToken cancellationToken);
}