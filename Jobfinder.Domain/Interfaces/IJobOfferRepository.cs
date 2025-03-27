using Jobfinder.Domain.Dtos.JobOffer;
using Jobfinder.Domain.Entities;

namespace Jobfinder.Domain.Interfaces;

public interface IJobOfferRepository
{
    Task<bool> CreateJobOffer(Guid employeeId,CreateJobOfferDto jobOfferDto, CancellationToken cancellationToken);
    Task<bool> UpdateJobOffer(Guid jobOfferId,UpdateJobOfferDto jobOfferDto, CancellationToken cancellationToken);
    Task<JobOffer?> GetJobOfferById(Guid jobOfferId, CancellationToken cancellationToken);
    Task<List<JobOffer>?> GetJobOffers(CancellationToken cancellationToken);
}