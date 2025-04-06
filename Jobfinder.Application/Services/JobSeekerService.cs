using Jobfinder.Application.Commons;
using Jobfinder.Application.Dtos.Cvs;
using Jobfinder.Application.Dtos.JobApplication;
using Jobfinder.Application.Dtos.JobOffer;
using Jobfinder.Application.Interfaces.Repositories;
using Jobfinder.Application.Interfaces.UnitOfWorks;
using Jobfinder.Domain.Entities;

namespace Jobfinder.Application.Services;

public sealed class JobSeekerService (ICvRepository cvRepository, IJobSeekerProfileRepository jobSeekerRepository,
    IJobOfferApplicationsUnitOfWork jobOfferApplicationsUnitOfWork)
{
    public async Task<Response<string>> CreateCv(CreateCvDto cvDto, Guid jobSeekerId, CancellationToken cancellationToken)
    {
        var jobSeeker = await jobSeekerRepository.GetProfileByUserId(jobSeekerId, cancellationToken);
        if (jobSeeker is null)
            return Response<string>.Failure("Job seeker does not exist");
        var cv = new Cv(cvDto.Location, cvDto.BirthDay, cvDto.MinimumSalary, cvDto.MaximumSalary, cvDto.Status, jobSeeker);
        await cvRepository.CreateCv(cv);
        if (await cvRepository.SaveChangesAsync(cancellationToken))
            return Response<string>.Success("Cv created");
        return Response<string>.Failure("Something went wrong");
    }


    public async Task<Response<string>> ApplyToJob(CreateJobApplicationDto dto, CancellationToken cancellationToken)
    {
        var result = await jobOfferApplicationsUnitOfWork.ApplyToJob(dto, cancellationToken);
        return result.IsSuccess ?
            Response<string>.Success() :
            Response<string>.Failure(result.Errors);
    }

    public async Task<Response<string>> CancelApplication(Guid jobId, Guid applicationId, Guid jobSeekerId)
    {
        var result = await jobOfferApplicationsUnitOfWork.CancelApplicationToJob(jobId, applicationId, jobSeekerId);
        return result.IsSuccess ?
            Response<string>.Success() :
            Response<string>.Failure(result.Errors);
    }
    
}