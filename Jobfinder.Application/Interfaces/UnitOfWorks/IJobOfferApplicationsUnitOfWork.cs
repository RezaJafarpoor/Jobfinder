using Jobfinder.Application.Commons;
using Jobfinder.Application.Dtos.JobApplication;
using Jobfinder.Domain.Entities;

namespace Jobfinder.Application.Interfaces.UnitOfWorks;

public interface IJobOfferApplicationsUnitOfWork
{
    Task<Response<string>> ApplyToJob(CreateJobApplicationDto dto,CancellationToken cancellationToken);
    Task<Response<string>> CancelApplicationToJob(CreateJobApplicationDto dto);
    Task<Response<List<Cv?>>> GetApplicationsForJob(Guid jobOfferId,CancellationToken cancellationToken);
}