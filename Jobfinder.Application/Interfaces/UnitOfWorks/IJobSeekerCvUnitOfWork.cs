using Jobfinder.Application.Commons;
using Jobfinder.Application.Dtos.Cv;

namespace Jobfinder.Application.Interfaces.UnitOfWorks;

public interface IJobSeekerCvUnitOfWork
{
    Task<Response<string>> CreateCvAndUpdateUsername(CreateCvDto cvDto, Guid jobSeekerId,
        CancellationToken cancellationToken);
}