using Jobfinder.Application.Commons;
using Jobfinder.Application.Dtos.JobApplication;
using Jobfinder.Application.Interfaces.Repositories;
using Jobfinder.Application.Interfaces.UnitOfWorks;
using Jobfinder.Domain.Entities;
using Jobfinder.Infrastructure.Persistence;

namespace Jobfinder.Infrastructure.UnitOfWorks;

internal class JobOfferApplicationsUnitOfWork
    (IJobOfferRepository jobOfferRepository,
        IJobSeekerProfileRepository jobSeekerRepository,
        IJobApplicationRepository jobApplicationRepository,
         ApplicationDbContext dbContext) : IJobOfferApplicationsUnitOfWork
{
    public async Task<Response<string>> ApplyToJob(CreateJobApplicationDto dto, CancellationToken cancellationToken)
    {
        await using var transaction = await dbContext.Database.BeginTransactionAsync(cancellationToken);
        try
        {
            var jobOffer = await jobOfferRepository.GetJobOfferById(dto.JobOfferId,cancellationToken);
            if (jobOffer is null)
                return Response<string>.Failure("Job does not exist");
            var jobSeeker = await jobSeekerRepository.GetProfileById(dto.JobSeekerProfileId, cancellationToken);
            if (jobSeeker is null)
                return Response<string>.Failure("User profile does not exist");
            var application = new JobApplication(jobSeeker, jobOffer);
            await jobApplicationRepository.AddJobApplication(application);
            await dbContext.SaveChangesAsync(cancellationToken);
            jobOffer.AddApplication(application);
            await jobOfferRepository.UpdateJobOffer(jobOffer);
            await dbContext.SaveChangesAsync(cancellationToken);
            await transaction.CommitAsync(cancellationToken);
            return Response<string>.Success("Applied successfully");
        }
        catch (Exception e)
        {
            await transaction.RollbackAsync(cancellationToken);
            return Response<string>.Failure(e.Message);
        }
    }

    public async Task<Response<string>> CancelApplicationToJob(Guid jobId, Guid applicationId, Guid jobSeekerId)
    {
        var result = await jobApplicationRepository.DeleteJobApplicationByJobSeekerId(jobId, applicationId, jobSeekerId);
        return result >= 1 ? 
            Response<string>.Success() :
            Response<string>.Failure("Something went wrong");
    }

    public async Task<Response<List<Cv?>>> GetApplicationsForJob(Guid jobOfferId, CancellationToken cancellationToken)
    {
        var cvs = await jobApplicationRepository.GetCvsForJobOffer(jobOfferId, cancellationToken);
        return cvs.Count == 0 ? Response<List<Cv?>>.Failure("Job offer does not exist") : Response<List<Cv?>>.Success(cvs);
    }
}