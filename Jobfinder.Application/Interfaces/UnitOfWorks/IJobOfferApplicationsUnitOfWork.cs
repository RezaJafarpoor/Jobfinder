using Jobfinder.Application.Interfaces.Common;
using Jobfinder.Application.Interfaces.Repositories;

namespace Jobfinder.Application.Interfaces.UnitOfWorks;

public interface IJobOfferApplicationsUnitOfWork : IScopedService
{
    public IJobApplicationRepository JobApplicationRepository { get; set; }
    public IJobSeekerProfileRepository JobSeekerProfileRepository { get; set; }
    public IJobOfferRepository JobOfferRepository { get; set; }
   
    Task<bool> SaveChangesAsync();
}