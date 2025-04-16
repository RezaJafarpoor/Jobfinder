using Jobfinder.Application.Interfaces.Repositories;
using Jobfinder.Domain.Entities;
using Jobfinder.Infrastructure.Persistence;
using Jobfinder.Infrastructure.Persistence.SqlServer;
using Microsoft.EntityFrameworkCore;

namespace Jobfinder.Infrastructure.Repositories;

internal class JobOfferRepository
    (ApplicationDbContext dbContext) : IJobOfferRepository
{
    public  Task CreateJobOffer( JobOffer jobOffer)
    {
        dbContext.JobOffers.Add(jobOffer);
        return Task.CompletedTask;
    }

    public  Task UpdateJobOffer(JobOffer jobOffer)
    {
        dbContext.JobOffers.Update(jobOffer);
        return Task.CompletedTask;
    }

    public async Task<JobOffer?> GetJobOfferById(Guid jobOfferId, CancellationToken cancellationToken)
    {
        var jobOffer = await dbContext.JobOffers.FirstOrDefaultAsync(jo => jo.Id == jobOfferId,cancellationToken);
        return jobOffer;
    }

    public async Task<List<JobOffer>?> GetJobOffers(CancellationToken cancellationToken)
    {
        var jobOffers = await dbContext.JobOffers.AsNoTracking().ToListAsync(cancellationToken);
        return jobOffers;
    }

    public Task DeleteJobOffer(JobOffer jobOffer)
    {
        dbContext.Remove(jobOffer);
        return Task.CompletedTask;
    }

    public async Task<bool> SaveChangesAsync(CancellationToken cancellationToken)
        => await dbContext.SaveChangesAsync(cancellationToken) > 0;
}