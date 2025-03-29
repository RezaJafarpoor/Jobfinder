using Jobfinder.Domain.Entities;

namespace Jobfinder.Application.Interfaces.Repositories;

public interface IJobOfferRepository
{
    Task CreateJobOffer(JobOffer jobOffer);
    Task UpdateJobOffer(JobOffer jobOffer);
    Task<JobOffer?> GetJobOfferById(Guid jobOfferId, CancellationToken cancellationToken);
    Task<List<JobOffer>?> GetJobOffers(CancellationToken cancellationToken);
    Task<bool> SaveChangesAsync(CancellationToken cancellationToken);
}