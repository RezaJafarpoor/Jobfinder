using Jobfinder.Domain.Entities;

namespace Jobfinder.Domain.Interfaces;

public interface IJobOfferRepository
{
    Task<bool> CreateJobOffer();
    Task<bool> UpdateJobOffer();
    Task<JobOffer> GetJobOfferById(Guid id);
    Task<List<JobOffer>> GetJobOffers();
}