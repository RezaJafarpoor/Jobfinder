using Jobfinder.Application.Interfaces.Repositories;
using Jobfinder.Domain.Entities;
using Jobfinder.Domain.Enums;
using Jobfinder.Infrastructure.Persistence;
using Microsoft.EntityFrameworkCore;

namespace Jobfinder.Infrastructure.Repositories;

internal class JobApplicationRepository
    (ApplicationDbContext dbContext) : IJobApplicationRepository
{
    public Task AddJobApplication(JobApplication application)
    {
        dbContext.Add(application);
        return Task.CompletedTask;
    }


    public async Task<JobApplication?> Test()
    {
        var application = await dbContext.JobApplications
            .Include(ja => ja.JobSeekerProfile)
            .ThenInclude(u => u.User)
            .FirstOrDefaultAsync();
            return application;
    }
    public async Task<List<JobApplication>> GetJobApplications(CancellationToken cancellationToken)
    {
        var applications = await dbContext.JobApplications.ToListAsync(cancellationToken);
        return applications;
    }
    public async Task<JobApplication?> GetJobApplication(Guid id,CancellationToken cancellationToken)
    {
        var application = await dbContext.JobApplications
            .Include(ja => ja.JobOffer)
            .FirstOrDefaultAsync(ja => ja.Id == id, cancellationToken);
        return application;
    }

    public async Task<JobApplication?> GetJobApplication(Guid jobId, Guid applicationId, CancellationToken cancellationToken)
    {
        var application = await dbContext.JobApplications
            .Include(ja => ja.JobSeekerProfile)
            .ThenInclude(u => u.User)
            .FirstOrDefaultAsync(ja => ja.Id == applicationId && ja.JobOfferId == jobId, cancellationToken);
        return application;
    }

    public async Task<int> DeleteJobApplicationByJobSeekerId(Guid jobId, Guid applicationId, Guid jobSeekerId)
    {
        var result = await dbContext.JobApplications
            .Where(ja => ja.JobSeekerProfileId == jobSeekerId && ja.JobOfferId ==jobId && ja.Id == applicationId)
            .ExecuteDeleteAsync();
        return result;
    }

    public async Task<List<Cv?>?> GetCvsForJobOffer(Guid jobOfferId, CancellationToken cancellationToken)
    {
        var cvs = await dbContext.JobApplications.Include(ja => ja.JobSeekerProfile)
            .ThenInclude(jsp => jsp.JobSeekerCv).Select(ja => ja.JobSeekerProfile.JobSeekerCv).ToListAsync(cancellationToken);
        return cvs;
    }

    public async Task<bool> SaveChangesAsync(CancellationToken cancellationToken) 
        => await dbContext.SaveChangesAsync(cancellationToken) > 0;

    
    
    
    
}