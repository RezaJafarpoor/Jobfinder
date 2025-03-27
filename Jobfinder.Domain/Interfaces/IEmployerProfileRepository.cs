using Jobfinder.Domain.Entities;

namespace Jobfinder.Domain.Interfaces;

public interface IEmployerProfileRepository
{
    Task<bool> UpdateEmployerProfile(Guid employerProfileId);
    Task<List<EmployerProfile>> GetEmployerProfiles(CancellationToken cancellationToken);
    Task<bool> Accept(Guid jobApplication, CancellationToken cancellationToken);
    Task<bool> Reject(Guid jobApplication, CancellationToken cancellationToken);
    Task<List<JobApplication>> GetJobApplications(Guid jobOfferId, CancellationToken cancellationToken);

}