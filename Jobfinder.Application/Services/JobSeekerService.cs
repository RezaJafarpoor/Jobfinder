using Jobfinder.Application.Commons;
using Jobfinder.Application.Dtos.Cvs;
using Jobfinder.Application.Dtos.JobApplication;
using Jobfinder.Application.Dtos.JobOffer;
using Jobfinder.Application.Interfaces.Repositories;
using Jobfinder.Application.Interfaces.UnitOfWorks;
using Jobfinder.Domain.Entities;

namespace Jobfinder.Application.Services;

public sealed class JobSeekerService (IJobOfferApplicationsUnitOfWork jobOfferApplicationsUnitOfWork,
    IJobSeekerCvUnitOfWork unitOfWork)
{
    public async Task<Response<string>> CreateCv(CreateCvDto cvDto, Guid jobSeekerId, CancellationToken cancellationToken)
    {
        var jobSeeker = await unitOfWork.JobSeekerProfileRepository.GetProfileByUserId(jobSeekerId, cancellationToken);
        if (jobSeeker is null)
            return Response<string>.Failure("Job seeker does not exist");
        var cv = new Cv(cvDto.Location, cvDto.BirthDay, cvDto.MinimumSalary, cvDto.MaximumSalary, cvDto.Status, jobSeeker);
        await unitOfWork.CvRepository.CreateCv(cv);
        if (await unitOfWork.SaveChangesAsync())
            return Response<string>.Success("Cv created");
        return Response<string>.Failure("Something went wrong");
    }

    public async Task<Response<string>> CreateCvAndUpdateUsername(CreateCvDto cvDto, Guid userId, CancellationToken cancellationToken)
    {
        await unitOfWork.BeginTransaction();
        var profile = await unitOfWork.JobSeekerProfileRepository.GetProfileByUserId(userId, cancellationToken);
        if (profile is null)
            return Response<string>.Failure("Job seeker does not exist");
        var cv = new Cv(cvDto.Location, cvDto.BirthDay, cvDto.MinimumSalary, cvDto.MaximumSalary, cvDto.Status, profile);
        await unitOfWork.CvRepository.CreateCv(cv);
        if (await unitOfWork.SaveChangesAsync())
            return Response<string>.Success("Cv created");
        return Response<string>.Failure("Something went wrong");
    }


    public async Task<Response<string>> ApplyToJob(CreateJobApplicationDto dto, CancellationToken cancellationToken)
    {
        throw new NotImplementedException();
    }
    

    public async Task<Response<string>> CancelApplication(Guid jobId, Guid applicationId, Guid jobSeekerId)
    {
        throw new NotImplementedException();
    }
    
}