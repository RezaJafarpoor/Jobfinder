using Azure.Core;
using Jobfinder.Domain.Dtos.JobOffer;
using Jobfinder.Domain.Entities;
using Jobfinder.Domain.Interfaces;
using Jobfinder.Infrastructure.Persistence;
using Microsoft.EntityFrameworkCore;

namespace Jobfinder.Infrastructure.Repositories;

internal class JobOfferRepository
    (ApplicationDbContext dbContext) : IJobOfferRepository
{
    public async Task<bool> CreateJobOffer(Guid employeeId, CreateJobOfferDto jobOfferDto, CancellationToken cancellationToken)
    {
        //TODO: Get category from database, getCompany name from employer profile
        var jobOffer = new JobOffer
        {
            JobName = jobOfferDto.JobName,
            JobDescription = jobOfferDto.JobDescription,
            JobDetails = jobOfferDto.JobDetails,
            Salary = jobOfferDto.Salary,
            CompanyName = jobOfferDto.CompanyName,
            Category = jobOfferDto.JobCategory,
            EmployerProfileId = employeeId,
        };
        dbContext.JobOffers.Add(jobOffer);
        return await dbContext.SaveChangesAsync(cancellationToken) > 0;
    }

    public async Task<bool> UpdateJobOffer(Guid jobOfferId,UpdateJobOfferDto jobOfferDto, CancellationToken cancellationToken)
    {
        var jobOffer = await dbContext.JobOffers.FirstOrDefaultAsync(jo => jo.Id == jobOfferId, cancellationToken);
        if (jobOffer is null)
            return false;
        jobOffer.JobDescription = jobOfferDto.JobDescription ?? jobOffer.JobDescription;
        jobOffer.JobDetails = jobOfferDto.JobDetails ?? jobOffer.JobDetails;
        return await dbContext.SaveChangesAsync(cancellationToken) > 0;
    }

    public async Task<JobOffer?> GetJobOfferById(Guid jobOfferId, CancellationToken cancellationToken)
    {
        var jobOffer = await dbContext.JobOffers.FirstOrDefaultAsync(jo => jo.Id == jobOfferId,cancellationToken);
        return jobOffer;
    }

    public async Task<List<JobOffer>?> GetJobOffers(CancellationToken cancellationToken)
    {
        var jobOffers = await dbContext.JobOffers.ToListAsync(cancellationToken);
        return jobOffers;
    }
}