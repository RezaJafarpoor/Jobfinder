using Jobfinder.Application.Interfaces.Common;
using Jobfinder.Application.Interfaces.Repositories;

namespace Jobfinder.Application.Interfaces.UnitOfWorks;

public interface IJobOfferApplicationsUnitOfWork : IScopedService, IDisposable
{
    public IJobApplicationRepository JobApplicationRepository { get; set; }
    public IJobSeekerProfileRepository JobSeekerProfileRepository { get; set; }
    Task BeginTransactionAsync();
    Task CommitAsync();
    Task RollbackAsync();
    Task SaveChangesAsync();
}